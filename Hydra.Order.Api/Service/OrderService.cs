using Hydra.Kernel.Enums;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Hydra.Ecommerce.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Hydra.Kernel.Extension;

namespace Hydra.Order.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public OrderService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<OrderModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<OrderModel>>();

            var list = await (from order in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.User)
                    .Include(x => x.ShippingMethod)
                    .Include(x => x.OrderNotes)
                              join payment in _queryRepository.Table<Ecommerce.Core.Domain.Payment>() on order.Id equals payment.OrderId
                              into pays
                              from pay in pays.DefaultIfEmpty()

                              join shipment in _queryRepository.Table<Shipment>() on order.Id equals shipment.OrderId
                              into ships
                              from ship in ships.DefaultIfEmpty()

                              select new OrderModel()
                              {
                                  Id = order.Id,
                                  UserId = order.UserId,
                                  UserName = order.User.Name,
                                  ShipmentId = order.ShipmentId,
                                  AddressId = order.AddressId,
                                  AddressSnapshot = order.AddressSnapshot,
                                  ShippingMethodId = order.ShippingMethodId,
                                  ShippingMethodTitle = order.ShippingMethod.Name,
                                  OrderStatusId = order.OrderStatusId,
                                  ShippingStatusId = order.ShippingStatusId,
                                  PaymentStatusId = order.PaymentStatusId,
                                  PaymentMethodId = order.PaymentMethodId,
                                  UserCurrencyType = order.UserCurrencyType,
                                  ShippingTax = order.ShippingTax,
                                  ShippingAmount = order.ShippingAmount,
                                  ShippingAmountTax = order.ShippingAmountTax,
                                  TaxAmount = order.TaxAmount,
                                  DiscountAmount = order.DiscountAmount,
                                  TotalAmount = order.TotalAmount,
                                  FinalPrice = order.FinalPrice,
                                  RefundedAmount = order.RefundedAmount,
                                  CustomerIp = order.CustomerIp,
                                  AllowStoringCreditCardNumber = order.AllowStoringCreditCardNumber,
                                  PaidDateUtc = order.PaidDateUtc,
                                  Deleted = order.Deleted,
                                  CreatedOnUtc = order.CreatedOnUtc,
                                  PaymentDateUtc = pay.PaymentDateUtc,
                                  TransactionTrackingCode = pay.TransactionTrackingCode,
                                  PaymentTrackingCode = pay.PaymentTrackingCode,
                                  TrackingNumber = ship.TrackingNumber,
                                  OrderNotes = order.OrderNotes.Select(x => x.Note).ToList()

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<OrderModel>> GetById(int id)
        {
            var result = new Result<OrderModel>();
            var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().FirstOrDefaultAsync(x => x.Id == id);

            var orderModel = new OrderModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                ShipmentId = order.ShipmentId,
                AddressId = order.AddressId,
                AddressSnapshot = order.AddressSnapshot,
                ShippingMethodId = order.ShippingMethodId,
                OrderStatusId = order.OrderStatusId,
                ShippingStatusId = order.ShippingStatusId,
                PaymentStatusId = order.PaymentStatusId,
                PaymentMethodId = order.PaymentMethodId,
                UserCurrencyType = order.UserCurrencyType,
                ShippingTax = order.ShippingTax,
                ShippingAmount = order.ShippingAmount,
                ShippingAmountTax = order.ShippingAmountTax,
                TaxAmount = order.TaxAmount,
                DiscountAmount = order.DiscountAmount,
                TotalAmount = order.TotalAmount,
                FinalPrice = order.FinalPrice,
                RefundedAmount = order.RefundedAmount,
                CustomerIp = order.CustomerIp,
                AllowStoringCreditCardNumber = order.AllowStoringCreditCardNumber,
                PaidDateUtc = order.PaidDateUtc,
                Deleted = order.Deleted,
                CreatedOnUtc = order.CreatedOnUtc
            };
            result.Data = orderModel;

            return result;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<Result<OrderModel>> Add(OrderModel orderModel)
        {
            var result = new Result<OrderModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().AnyAsync(x => x.Id == orderModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(orderModel.Id), "The Id already exist"));
                    return result;
                }
                var order = new Ecommerce.Core.Domain.Order()
                {
                    UserId = orderModel.UserId,
                    ShipmentId = orderModel.ShipmentId,
                    AddressId = orderModel.AddressId,
                    ShippingMethodId = orderModel.ShippingMethodId,
                    OrderStatusId = orderModel.OrderStatusId,
                    ShippingStatusId = orderModel.ShippingStatusId,
                    PaymentStatusId = orderModel.PaymentStatusId,
                    PaymentMethodId = orderModel.PaymentMethodId,
                    UserCurrencyType = orderModel.UserCurrencyType,
                    ShippingTax = orderModel.ShippingTax,
                    ShippingAmount = orderModel.ShippingAmount,
                    ShippingAmountTax = orderModel.ShippingAmountTax,
                    TaxAmount = orderModel.TaxAmount,
                    DiscountAmount = orderModel.DiscountAmount,
                    TotalAmount = orderModel.TotalAmount,
                    FinalPrice = orderModel.FinalPrice,
                    RefundedAmount = orderModel.RefundedAmount,
                    CustomerIp = orderModel.CustomerIp,
                    AllowStoringCreditCardNumber = orderModel.AllowStoringCreditCardNumber,
                    PaidDateUtc = orderModel.PaidDateUtc,
                    Deleted = orderModel.Deleted,
                    CreatedOnUtc = orderModel.CreatedOnUtc,
                };

                await _commandRepository.InsertAsync(order);
                await _commandRepository.SaveChangesAsync();

                orderModel.Id = order.Id;

                result.Data = orderModel;

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
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<Result<OrderModel>> Update(OrderModel orderModel)
        {
            var result = new Result<OrderModel>();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().FirstOrDefaultAsync(x => x.Id == orderModel.Id);
                if (order is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Order not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().AnyAsync(x => x.Id != orderModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(orderModel.Id), "The Id already exist"));
                    return result;
                }
                order.UserId = orderModel.UserId;
                order.ShipmentId = orderModel.ShipmentId;
                order.AddressId = orderModel.AddressId;
                order.ShippingMethodId = orderModel.ShippingMethodId;
                order.OrderStatusId = (OrderStatus)orderModel.OrderStatusId;
                order.ShippingStatusId = (ShippingStatus)orderModel.ShippingStatusId;
                order.PaymentStatusId = orderModel.PaymentStatusId;
                order.PaymentMethodId = orderModel.PaymentMethodId;
                order.UserCurrencyType = orderModel.UserCurrencyType;
                order.ShippingTax = order.ShippingTax;
                order.ShippingAmount = order.ShippingAmount;
                order.ShippingAmountTax = order.ShippingAmountTax;
                order.TaxAmount = order.TaxAmount;
                order.DiscountAmount = order.DiscountAmount;
                order.TotalAmount = order.TotalAmount;
                order.FinalPrice = order.FinalPrice;
                order.RefundedAmount = orderModel.RefundedAmount;
                order.CustomerIp = orderModel.CustomerIp;
                order.AllowStoringCreditCardNumber = orderModel.AllowStoringCreditCardNumber;
                order.PaidDateUtc = orderModel.PaidDateUtc;
                order.Deleted = orderModel.Deleted;
                order.CreatedOnUtc = orderModel.CreatedOnUtc;

                _commandRepository.Update(order);
                await _commandRepository.SaveChangesAsync();

                result.Data = orderModel;

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
        /// 
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<Result<OrderModel>> UpdateState(OrderModel orderModel)
        {
            var result = new Result<OrderModel>();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().FirstOrDefaultAsync(x => x.Id == orderModel.Id);
                if (order is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Order not found";
                    return result;
                }

                order.ShippingMethodId = orderModel.ShippingMethodId;
                order.OrderStatusId = (OrderStatus)orderModel.OrderStatusId;
                order.ShippingStatusId = (ShippingStatus)orderModel.ShippingStatusId;
                order.PaymentStatusId = orderModel.PaymentStatusId;

                _commandRepository.Update(order);
                await _commandRepository.SaveChangesAsync();

                result.Data = orderModel;

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
            var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Order not found";
                return result;
            }

            _commandRepository.Delete(order);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

        public async Task<Result<List<OrderStatusModel>>> GetAllOrderStatus()
        {
            return await Task.Run(() =>
            {
                var result = new Result<List<OrderStatusModel>>();

                var orderStatus = Enum.GetValues(typeof(OrderStatus)).Cast<Enum>()
                    .Select(x => new OrderStatusModel
                    {
                        Id = Convert.ToInt32(x),
                        Title = x.ToString()
                    }).ToList();

                result.Data = orderStatus;

                return result;
            });
        }

        public async Task<Result<List<ShippingStatusModel>>> GetAllShippingStatus()
        {
            return await Task.Run(() =>
            {
                var result = new Result<List<ShippingStatusModel>>();

                var shippingStatus = Enum.GetValues(typeof(ShippingStatus)).Cast<Enum>()
                    .Select(x => new ShippingStatusModel
                    {
                        Id = Convert.ToInt32(x),
                        Title = x.ToString()
                    }).ToList();

                result.Data = shippingStatus;

                return result;
            });
        }

        // --- User-facing methods ---

        public async Task<Result<List<OrderModel>>> GetMyOrders(int userId)
        {
            var result = new Result<List<OrderModel>>();

            var list = await (from order in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.User)
                    .Include(x => x.ShippingMethod)
                    .Include(x => x.OrderNotes)
                    .Where(x => x.UserId == userId)
                              join payment in _queryRepository.Table<Ecommerce.Core.Domain.Payment>() on order.Id equals payment.OrderId
                              into pays
                              from pay in pays.DefaultIfEmpty()

                              join shipment in _queryRepository.Table<Shipment>() on order.Id equals shipment.OrderId
                              into ships
                              from ship in ships.DefaultIfEmpty()

                              select new OrderModel()
                              {
                                  Id = order.Id,
                                  UserId = order.UserId,
                                  UserName = order.User.Name,
                                  ShipmentId = order.ShipmentId,
                                  AddressId = order.AddressId,
                                  AddressSnapshot = order.AddressSnapshot,
                                  ShippingMethodId = order.ShippingMethodId,
                                  ShippingMethodTitle = order.ShippingMethod.Name,
                                  OrderStatusId = order.OrderStatusId,
                                  ShippingStatusId = order.ShippingStatusId,
                                  PaymentStatusId = order.PaymentStatusId,
                                  PaymentMethodId = order.PaymentMethodId,
                                  UserCurrencyType = order.UserCurrencyType,
                                  ShippingTax = order.ShippingTax,
                                  ShippingAmount = order.ShippingAmount,
                                  ShippingAmountTax = order.ShippingAmountTax,
                                  TaxAmount = order.TaxAmount,
                                  DiscountAmount = order.DiscountAmount,
                                  TotalAmount = order.TotalAmount,
                                  FinalPrice = order.FinalPrice,
                                  RefundedAmount = order.RefundedAmount,
                                  CustomerIp = order.CustomerIp,
                                  AllowStoringCreditCardNumber = order.AllowStoringCreditCardNumber,
                                  PaidDateUtc = order.PaidDateUtc,
                                  Deleted = order.Deleted,
                                  CreatedOnUtc = order.CreatedOnUtc,
                                  PaymentDateUtc = pay.PaymentDateUtc,
                                  TransactionTrackingCode = pay.TransactionTrackingCode,
                                  PaymentTrackingCode = pay.PaymentTrackingCode,
                                  TrackingNumber = ship.TrackingNumber,
                                  OrderNotes = order.OrderNotes.Select(x => x.Note).ToList()
                              }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;
            return result;
        }

        public async Task<Result<OrderModel>> GetMyOrderById(int userId, int orderId)
        {
            var result = new Result<OrderModel>();

            var order = await (from o in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.User)
                    .Include(x => x.ShippingMethod)
                    .Include(x => x.OrderNotes)
                    .Where(x => x.Id == orderId && x.UserId == userId)
                               join payment in _queryRepository.Table<Ecommerce.Core.Domain.Payment>() on o.Id equals payment.OrderId
                               into pays
                               from pay in pays.DefaultIfEmpty()
                               join shipment in _queryRepository.Table<Shipment>() on o.Id equals shipment.OrderId
                               into ships
                               from ship in ships.DefaultIfEmpty()
                               select new OrderModel()
                               {
                                   Id = o.Id,
                                   UserId = o.UserId,
                                   UserName = o.User.Name,
                                   ShipmentId = o.ShipmentId,
                                   AddressId = o.AddressId,
                                   AddressSnapshot = o.AddressSnapshot,
                                   ShippingMethodId = o.ShippingMethodId,
                                   ShippingMethodTitle = o.ShippingMethod.Name,
                                   OrderStatusId = o.OrderStatusId,
                                   ShippingStatusId = o.ShippingStatusId,
                                   PaymentStatusId = o.PaymentStatusId,
                                   PaymentMethodId = o.PaymentMethodId,
                                   UserCurrencyType = o.UserCurrencyType,
                                   ShippingTax = o.ShippingTax,
                                   ShippingAmount = o.ShippingAmount,
                                   ShippingAmountTax = o.ShippingAmountTax,
                                   TaxAmount = o.TaxAmount,
                                   DiscountAmount = o.DiscountAmount,
                                   TotalAmount = o.TotalAmount,
                                   FinalPrice = o.FinalPrice,
                                   RefundedAmount = o.RefundedAmount,
                                   CustomerIp = o.CustomerIp,
                                   AllowStoringCreditCardNumber = o.AllowStoringCreditCardNumber,
                                   PaidDateUtc = o.PaidDateUtc,
                                   Deleted = o.Deleted,
                                   CreatedOnUtc = o.CreatedOnUtc,
                                   PaymentDateUtc = pay.PaymentDateUtc,
                                   TransactionTrackingCode = pay.TransactionTrackingCode,
                                   PaymentTrackingCode = pay.PaymentTrackingCode,
                                   TrackingNumber = ship.TrackingNumber,
                                   OrderNotes = o.OrderNotes.Select(x => x.Note).ToList()
                               }).FirstOrDefaultAsync();

            if (order is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "Order not found";
                return result;
            }

            result.Data = order;
            return result;
        }

        public async Task<Result<List<OrderItemModel>>> GetMyOrderItems(int userId, int orderId)
        {
            var result = new Result<List<OrderItemModel>>();

            var orderExists = await _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                .AnyAsync(x => x.Id == orderId && x.UserId == userId);

            if (!orderExists)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "Order not found";
                return result;
            }

            var items = await _queryRepository.Table<OrderItem>()
                .Include(x => x.ProductVariant)
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderItemModel()
                {
                    Id = x.Id,
                    OrderId = x.OrderId,
                    ProductId = x.ProductVariantId,
                    ProductName = x.ProductVariant.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    DiscountAmount = x.DiscountAmount,
                    TotalPrice = x.TotalPrice,
                    TotalPriceTax = x.TotalPriceTax,
                }).ToListAsync();

            result.Data = items;
            return result;
        }

        public async Task<Result<OrderModel>> CreateOrder(int userId, CreateOrderRequest request)
        {
            var result = new Result<OrderModel>();
            try
            {
                if (request.Items == null || request.Items.Count == 0)
                {
                    result.Status = ResultStatusEnum.Failed;
                    result.Message = "Order must contain at least one item";
                    return result;
                }

                // Snapshot the address at order creation time
                string addressSnapshot = null;
                if (request.AddressId.HasValue)
                {
                    var address = await _queryRepository.Table<Address>()
                        .Include(a => a.Country).Include(a => a.StateProvince)
                        .FirstOrDefaultAsync(a => a.Id == request.AddressId.Value);
                    if (address != null)
                    {
                        addressSnapshot = FormatAddress(address);
                    }
                }

                var order = new Ecommerce.Core.Domain.Order()
                {
                    UserId = userId,
                    AddressId = request.AddressId,
                    AddressSnapshot = addressSnapshot,
                    ShippingMethodId = request.ShippingMethodId,
                    PaymentMethodId = request.PaymentMethodId,
                    OrderStatusId = OrderStatus.Pending,
                    ShippingStatusId = 0,
                    PaymentStatusId = 0,
                    UserCurrencyType = CurrencyType.Dollar,
                    TotalAmount = request.Items.Sum(x => x.UnitPrice * x.Quantity),
                    FinalPrice = request.Items.Sum(x => x.UnitPrice * x.Quantity),
                    CreatedOnUtc = DateTime.UtcNow,
                };

                await _commandRepository.InsertAsync(order);
                await _commandRepository.SaveChangesAsync();

                var orderItems = request.Items.Select(item => new OrderItem()
                {
                    OrderId = order.Id,
                    ProductVariantId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.UnitPrice * item.Quantity,
                    TotalPriceTax = 0,
                    DiscountAmount = 0,
                }).ToList();

                await _commandRepository.InsertRangeAsync(orderItems);
                await _commandRepository.SaveChangesAsync();

                result.Data = new OrderModel()
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    AddressId = order.AddressId,
                    AddressSnapshot = order.AddressSnapshot,
                    ShippingMethodId = order.ShippingMethodId,
                    PaymentMethodId = order.PaymentMethodId,
                    OrderStatusId = order.OrderStatusId,
                    TotalAmount = order.TotalAmount,
                    FinalPrice = order.FinalPrice,
                    CreatedOnUtc = order.CreatedOnUtc,
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

        public async Task<Result> CancelMyOrder(int userId, int orderId)
        {
            var result = new Result();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);

                if (order is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "Order not found";
                    return result;
                }

                if (order.OrderStatusId != OrderStatus.Pending)
                {
                    result.Status = ResultStatusEnum.Failed;
                    result.Message = "Only pending orders can be cancelled";
                    return result;
                }

                order.OrderStatusId = OrderStatus.Cancelled;
                _commandRepository.Update(order);
                await _commandRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        private static string FormatAddress(Address address)
        {
            var parts = new List<string>();
            if (address.Country != null && !string.IsNullOrEmpty(address.Country.Name)) parts.Add(address.Country.Name);
            if (address.StateProvince != null && !string.IsNullOrEmpty(address.StateProvince.Name)) parts.Add(address.StateProvince.Name);
            if (!string.IsNullOrEmpty(address.City)) parts.Add(address.City);
            if (!string.IsNullOrEmpty(address.County)) parts.Add(address.County);
            if (!string.IsNullOrEmpty(address.Address1)) parts.Add(address.Address1);
            if (!string.IsNullOrEmpty(address.ZipPostalCode)) parts.Add(address.ZipPostalCode);
            if (!string.IsNullOrEmpty(address.PhoneNumber)) parts.Add("Ph: " + address.PhoneNumber);
            return string.Join(", ", parts);
        }
    }
}