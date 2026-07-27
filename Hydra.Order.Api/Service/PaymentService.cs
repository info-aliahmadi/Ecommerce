using System.Linq.Dynamic.Core;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Microsoft.EntityFrameworkCore;
using Hydra.Kernel.Extension;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Order.Core.Models;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Api.Services;


namespace Hydra.Payment.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        private readonly IOrderService _orderService;
        public PaymentService(IQueryRepository queryRepository, ICommandRepository commandRepository, IOrderService orderService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _orderService = orderService;
        }

        public async Task<Result<List<PaymentStatusModel>>> GetAllPaymentStatus()
        {
            return await Task.Run(() =>
            {
                var result = new Result<List<PaymentStatusModel>>();

                var paymentStatus = Enum.GetValues(typeof(PaymentStatus)).Cast<Enum>()
                    .Select(x => new PaymentStatusModel
                    {
                        Id = Convert.ToInt32(x),
                        Title = x.ToString()
                    }).ToList();

                result.Data = paymentStatus;

                return result;
            });
        }

        // --- User-facing methods ---

        public async Task<Result<List<PaymentModel>>> GetMyPayments(int userId)
        {
            var result = new Result<List<PaymentModel>>();

            var list = await (from payment in _queryRepository.Table<Ecommerce.Core.Domain.Payment>()
                              join order in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                                  on payment.OrderId equals order.Id
                              where order.UserId == userId
                              select new PaymentModel()
                              {
                                  Id = payment.Id,
                                  OrderId = payment.OrderId,
                                  TransactionTrackingCode = payment.TransactionTrackingCode,
                                  PaymentTrackingCode = payment.PaymentTrackingCode,
                                  PaymentDateUtc = payment.PaymentDateUtc,
                                  PaymentTypeId = payment.PaymentTypeId,
                                  Status = payment.Status,
                                  Deleted = payment.Deleted,
                                  CreatedOnUtc = payment.CreatedOnUtc,
                                  CardType = payment.CardType,
                                  CardName = payment.CardName,
                                  MaskedCreditCardNumber = payment.MaskedCreditCardNumber,
                              }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;
            return result;
        }

        public async Task<Result<PaymentViewModel>> GetMyPaymentById(int userId, int paymentId)
        {
            var result = new Result<PaymentViewModel>();

            var payment = await (from p in _queryRepository.Table<Ecommerce.Core.Domain.Payment>()
                                 join o in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                                     on p.OrderId equals o.Id
                                 where p.Id == paymentId && o.UserId == userId
                                 select new PaymentViewModel()
                                 {
                                     Id = p.Id,
                                     TransactionTrackingCode = p.TransactionTrackingCode,
                                     PaymentTrackingCode = p.PaymentTrackingCode,
                                     PaymentDateUtc = p.PaymentDateUtc,
                                     PaymentTypeId = p.PaymentTypeId,
                                     Status = p.Status,
                                     CardName = p.CardName,
                                     CardNumber = p.MaskedCreditCardNumber,
                                 }).FirstOrDefaultAsync();

            if (payment is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "Payment not found";
                return result;
            }

            result.Data = payment;
            return result;
        }

        public async Task<Result<PaymentModel>> ProcessPayment(int userId, ProcessPaymentRequest request)
        {
            var result = new Result<PaymentModel>();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .FirstOrDefaultAsync(x => x.Id == request.OrderId && x.UserId == userId);

                if (order is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "Order not found";
                    return result;
                }

                if (order.PaymentStatusId == PaymentStatus.Paid)
                {
                    result.Status = ResultStatusEnum.Failed;
                    result.Message = "Order is already paid";
                    return result;
                }

                var maskedCard = string.IsNullOrEmpty(request.CardNumber)
                    ? string.Empty
                    : request.CardNumber.Length > 4
                        ? new string('*', request.CardNumber.Length - 4) + request.CardNumber[^4..]
                        : request.CardNumber;

                var payment = new Ecommerce.Core.Domain.Payment()
                {
                    OrderId = request.OrderId,
                    PaymentTypeId = request.PaymentMethodId,
                    Status = PaymentStatus.Paid,
                    PaymentDateUtc = DateTime.UtcNow,
                    CardName = request.CardName,
                    CardNumber = request.CardNumber,
                    MaskedCreditCardNumber = maskedCard,
                    CardCvv2 = request.CardCvv2,
                    CardExpirationMonth = request.CardExpirationMonth,
                    CardExpirationYear = request.CardExpirationYear,
                    CreatedOnUtc = DateTime.UtcNow,
                };

                await _commandRepository.InsertAsync(payment);

                order.PaymentStatusId = PaymentStatus.Paid;
                order.PaymentMethodId = request.PaymentMethodId;
                order.PaidDateUtc = DateTime.UtcNow;
                _commandRepository.Update(order);

                await _commandRepository.SaveChangesAsync();

                await _orderService.ConfirmOrder(userId, request.OrderId);

                result.Data = new PaymentModel()
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    PaymentTypeId = payment.PaymentTypeId,
                    Status = payment.Status,
                    PaymentDateUtc = payment.PaymentDateUtc,
                    CardName = payment.CardName,
                    MaskedCreditCardNumber = payment.MaskedCreditCardNumber,
                    CreatedOnUtc = payment.CreatedOnUtc,
                };

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<PaymentModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<PaymentModel>>();

            var list = await (from payment in _queryRepository.Table<Ecommerce.Core.Domain.Payment>()
                              select new PaymentModel()
                              {
                                  Id = payment.Id,
                                  OrderId = payment.OrderId,
                                  TransactionTrackingCode = payment.TransactionTrackingCode,
                                  PaymentTrackingCode = payment.PaymentTrackingCode,
                                  PaymentDateUtc = payment.PaymentDateUtc,
                                  PaymentTypeId = payment.PaymentTypeId,
                                  Status = payment.Status,
                                  Deleted = payment.Deleted,
                                  CreatedOnUtc = payment.CreatedOnUtc,
                                  CardType = payment.CardType,
                                  CardName = payment.CardName,
                                  CardNumber = payment.CardNumber,
                                  MaskedCreditCardNumber = payment.MaskedCreditCardNumber,
                                  CardCvv2 = payment.CardCvv2,
                                  CardExpirationMonth = payment.CardExpirationMonth,
                                  CardExpirationYear = payment.CardExpirationYear,

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<PaymentModel>> GetById(int id)
        {
            var result = new Result<PaymentModel>();
            var payment = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().FirstOrDefaultAsync(x => x.Id == id);

            var paymentModel = new PaymentModel()
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                TransactionTrackingCode = payment.TransactionTrackingCode,
                PaymentTrackingCode = payment.PaymentTrackingCode,
                PaymentDateUtc = payment.PaymentDateUtc,
                PaymentTypeId = payment.PaymentTypeId,
                Status = payment.Status,
                Deleted = payment.Deleted,
                CreatedOnUtc = payment.CreatedOnUtc,
                CardType = payment.CardType,
                CardName = payment.CardName,
                CardNumber = payment.CardNumber,
                MaskedCreditCardNumber = payment.MaskedCreditCardNumber,
                CardCvv2 = payment.CardCvv2,
                CardExpirationMonth = payment.CardExpirationMonth,
                CardExpirationYear = payment.CardExpirationYear,

            };
            result.Data = paymentModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<PaymentViewModel>> GetOrderPaymentById(int id)
        {
            var result = new Result<PaymentViewModel>();
            var payment = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().FirstAsync(x => x.OrderId == id);

            var paymentModel = new PaymentViewModel
            {
                Id = payment.Id,
                TransactionTrackingCode = payment.TransactionTrackingCode,
                PaymentTrackingCode = payment.PaymentTrackingCode,
                PaymentDateUtc = payment.PaymentDateUtc,
                PaymentTypeId = payment.PaymentTypeId,
                Status = payment.Status,
                CardName = payment.CardName,
                CardNumber = payment.CardNumber
            };
            result.Data = paymentModel;

            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<Result<PaymentModel>> Add(PaymentModel paymentModel)
        {
            var result = new Result<PaymentModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().AnyAsync(x => x.Id == paymentModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(paymentModel.Id), "The Id already exist"));
                    return result;
                }
                var payment = new Ecommerce.Core.Domain.Payment()
                {
                    OrderId = paymentModel.OrderId,
                    TransactionTrackingCode = paymentModel.TransactionTrackingCode,
                    PaymentTrackingCode = paymentModel.PaymentTrackingCode,
                    PaymentDateUtc = paymentModel.PaymentDateUtc,
                    PaymentTypeId = paymentModel.PaymentTypeId,
                    Status = paymentModel.Status,
                    Deleted = paymentModel.Deleted,
                    CreatedOnUtc = paymentModel.CreatedOnUtc,
                    CardType = paymentModel.CardType,
                    CardName = paymentModel.CardName,
                    CardNumber = paymentModel.CardNumber,
                    MaskedCreditCardNumber = paymentModel.MaskedCreditCardNumber,
                    CardCvv2 = paymentModel.CardCvv2,
                    CardExpirationMonth = paymentModel.CardExpirationMonth,
                    CardExpirationYear = paymentModel.CardExpirationYear,

                };

                await _commandRepository.InsertAsync(payment);
                await _commandRepository.SaveChangesAsync();

                paymentModel.Id = payment.Id;

                result.Data = paymentModel;

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        public async Task<Result<PaymentModel>> Update(PaymentModel paymentModel)
        {
            var result = new Result<PaymentModel>();
            try
            {
                var payment = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().FirstAsync(x => x.Id == paymentModel.Id);
                if (payment is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Payment not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().AnyAsync(x => x.Id != paymentModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(paymentModel.Id), "The Id already exist"));
                    return result;
                }
                payment.OrderId = paymentModel.OrderId;
                payment.TransactionTrackingCode = paymentModel.TransactionTrackingCode;
                payment.PaymentTrackingCode = paymentModel.PaymentTrackingCode;
                payment.PaymentDateUtc = paymentModel.PaymentDateUtc;
                payment.PaymentTypeId = paymentModel.PaymentTypeId;
                payment.Status = paymentModel.Status;
                payment.Deleted = paymentModel.Deleted;
                payment.CreatedOnUtc = paymentModel.CreatedOnUtc;
                payment.CardType = paymentModel.CardType;
                payment.CardName = paymentModel.CardName;
                payment.CardNumber = paymentModel.CardNumber;
                payment.MaskedCreditCardNumber = paymentModel.MaskedCreditCardNumber;
                payment.CardCvv2 = paymentModel.CardCvv2;
                payment.CardExpirationMonth = paymentModel.CardExpirationMonth;
                payment.CardExpirationYear = paymentModel.CardExpirationYear;

                _commandRepository.Update(payment);
                await _commandRepository.SaveChangesAsync();

                result.Data = paymentModel;

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var payment = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>().FirstOrDefaultAsync(x => x.Id == id);
            if (payment is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Payment not found";
                return result;
            }

            _commandRepository.Delete(payment);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}