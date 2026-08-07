using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel;
using Hydra.Kernel.Enums;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Order.Core.Interfaces;
using Hydra.Order.Core.Models;
using Microsoft.EntityFrameworkCore;
using Twilio.TwiML.Voice;

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
                                  UserAvatar = order.User.Avatar,
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
                                  PaymentDateUtc = pay.PaymentDateUtc,
                                  Deleted = order.Deleted,
                                  CreatedOnUtc = order.CreatedOnUtc,
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
                UserName = order.User.Name,
                UserAvatar = order.User.Avatar,
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
        public async Task<Result<OrderChangeStatusModel>> UpdateState(OrderChangeStatusModel orderStatusModel)
        {
            var result = new Result<OrderChangeStatusModel>();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>().FirstOrDefaultAsync(x => x.Id == orderStatusModel.OrderId);
                if (order is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Order not found";
                    return result;
                }

                order.ShippingMethodId = orderStatusModel.ShippingMethodId;
                order.ShippingStatusId = orderStatusModel.ShippingStatusId;
                order.OrderStatusId = orderStatusModel.OrderStatusId;
                order.PaymentStatusId = orderStatusModel.PaymentStatusId;
                order.PaymentMethodId = orderStatusModel.PaymentMethodId;

                _commandRepository.Update(order);
                await _commandRepository.SaveChangesAsync();

                result.Data = orderStatusModel;

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

        // --- User-facing methods ---

        public async Task<Result<List<OrderModel>>> GetMyOrders(int userId)
        {
            var result = new Result<List<OrderModel>>();

            var list = await (from order in _queryRepository.Table<Ecommerce.Core.Domain.Order>().Include(x => x.OrderItems)
                                  .ThenInclude(x => x.ProductVariant).ThenInclude(x => x.Product)
                                  .Include(x => x.User)
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
                                  PaymentDateUtc = pay.PaymentDateUtc,
                                  Deleted = order.Deleted,
                                  CreatedOnUtc = order.CreatedOnUtc,
                                  TransactionTrackingCode = pay.TransactionTrackingCode,
                                  PaymentTrackingCode = pay.PaymentTrackingCode,
                                  TrackingNumber = ship.TrackingNumber,
                                  OrderNotes = order.OrderNotes.Select(x => x.Note).ToList(),
                                  Items = order.OrderItems.Select(x => new OrderItemModel()
                                  {
                                      Id = x.Id,
                                      OrderId = x.OrderId,
                                      ProductVariantId = x.ProductVariantId,
                                      ProductVariant = new Product.Core.Models.ProductVariantDisplayModel()
                                      {
                                          Id = x.ProductVariantId,
                                          SKU = x.ProductVariant.SKU,
                                          OldSellPrice = x.ProductVariant.OldSellPrice,
                                          ProductId = x.ProductVariant.ProductId,
                                          SellPrice = x.ProductVariant.SellPrice,
                                          ProductAttributes = x.ProductVariant.VariantAttributes.Select(v => new Product.Core.Models.ProductAttributeDisplayModel()
                                          {
                                              Id = v.Id,
                                              AttributeType = v.Attribute.AttributeType,
                                              Description = v.Attribute.Description,
                                              DisplayName = v.Attribute.DisplayName,
                                              DisplayOrder = v.Attribute.DisplayOrder,
                                              Key = v.Attribute.Key
                                          }).ToList(),
                                      },
                                      DiscountAmount = x.DiscountAmount,
                                      ProductImagePreview = new FileStorage.Core.Models.FileUploadModel(x.ProductVariant.Product.ImagePreview),
                                      ProductName = x.ProductVariant.Product.Name,
                                      Quantity = x.Quantity,
                                      TotalPrice = x.TotalPrice,
                                      TotalPriceTax = x.TotalPriceTax,
                                      UnitPrice = x.UnitPrice
                                  }).ToList()
                              }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;
            return result;
        }

        public async Task<Result<OrderModel>> GetMyOrderById(int userId, int orderId)
        {
            var result = new Result<OrderModel>();

            var order = await (from o in _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.User)
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
                    ProductVariantId = x.ProductVariantId,
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
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

                var variantIds = request.Items.Select(x => x.ProductVariantId).Distinct().ToList();
                var variants = await _queryRepository.Table<ProductVariant>()
                    .Include(x => x.Product.ProductCategories)
                    .Include(x => x.Product.ProductManufacturers)
                    .Include(x => x.Product)
                    .Include(x => x.ProductInventory)
                    .Where(x => variantIds.Contains(x.Id))
                    .ToListAsync();

                foreach (var item in request.Items)
                {
                    var variant = variants.FirstOrDefault(x => x.Id == item.ProductVariantId);
                    if (variant == null)
                    {
                        result.Status = ResultStatusEnum.NotFound;
                        result.Message = $"Product variant {item.ProductVariantId} not found";
                        return result;
                    }
                    if (item.UnitPrice != variant.SellPrice)
                    {
                        result.Status = ResultStatusEnum.InvalidValidation;
                        result.Message = $"Unit price for product variant {item.ProductVariantId} does not match the real price";
                        return result;
                    }
                }


                using var transaction = await _commandRepository.BeginTransactionAsync();

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

                var currentDatetime = DateTime.UtcNow;
                var order = new Ecommerce.Core.Domain.Order()
                {
                    UserId = userId,
                    CustomerIp = request.CustomerIp,
                    AddressId = request.AddressId,
                    AddressSnapshot = addressSnapshot,
                    ShippingMethodId = request.ShippingMethodId,
                    PaymentMethodId = request.PaymentMethodId,
                    OrderStatusId = request.PaymentMethodId == PaymentMethod.CashOnDelivery ? OrderStatus.Processing : OrderStatus.Pending,

                    ShippingStatusId = ShippingStatus.NotYetShipped,
                    PaymentStatusId = request.PaymentMethodId == PaymentMethod.CashOnDelivery ? PaymentStatus.Authorized : PaymentStatus.Pending,
                    UserCurrencyType = DefaultSetting.DEFAULT_CURRENCY,
                    TotalAmount = request.Items.Sum(x => x.UnitPrice * x.Quantity),
                    FinalPrice = request.Items.Sum(x => (x.UnitPrice * x.Quantity)), //   after Discount and Tax
                    CreatedOnUtc = currentDatetime
                };

                await _commandRepository.InsertAsync(order);
                await _commandRepository.SaveChangesAsync();
                if (!string.IsNullOrEmpty(request.OrderNote?.Trim()))
                {
                    var orderNote = new Ecommerce.Core.Domain.OrderNote()
                    {
                        OrderId = order.Id,
                        UserId = userId,
                        Note = request.OrderNote,
                        IsRead = false,
                        CreatedOnUtc = currentDatetime
                    };

                    await _commandRepository.InsertAsync(orderNote);
                    await _commandRepository.SaveChangesAsync();
                }
                var itemDiscounts = new Dictionary<int, decimal>();
                decimal totalOrderDiscount = 0;
                if (request.DiscountId != null)
                {
                    var discount = await _queryRepository.Table<Discount>()
                        .Include(x => x.DiscountProducts)
                        .Include(x => x.DiscountCategories)
                        .Include(x => x.DiscountManufacturers)
                        .FirstOrDefaultAsync(x => x.Id == request.DiscountId.Value);



                    if (discount != null && discount.IsActive)
                    {
                        bool isValidDate = (!discount.StartDateUtc.HasValue || discount.StartDateUtc <= currentDatetime) &&
                                          (!discount.EndDateUtc.HasValue || discount.EndDateUtc >= currentDatetime);

                        if (discount.DiscountLimitationId == DiscountLimitationType.NTimesOnly)
                        {
                            var limitationTimes = discount.LimitationTimes;
                            var discountUsedTimes = _queryRepository.Table<OrderDiscount>().Where(x => x.DiscountId == discount.Id).Count();
                            if (discountUsedTimes > discount.LimitationTimes)
                            {
                                isValidDate = false;
                            }
                        }

                        if (discount.DiscountLimitationId == DiscountLimitationType.NTimesPerCustomer)
                        {
                            var limitationTimes = discount.LimitationTimes;
                            var discountUserCount = _queryRepository.Table<OrderDiscount>().Where(x => x.DiscountId == discount.Id && x.Order.UserId == userId).Count();
                            if (discountUserCount > discount.LimitationTimes)
                            {
                                isValidDate = false;
                            }
                        }

                        if (isValidDate)
                        {

                            var eligibleItems = new List<(int ProductVariantId, decimal ItemTotal)>();
                            foreach (var item in request.Items)
                            {
                                var variant = variants.FirstOrDefault(x => x.Id == item.ProductVariantId);
                                if (variant == null) continue;

                                bool isEligible = discount.DiscountTypeId switch
                                {
                                    DiscountType.AssignedToCouponCode => true,
                                    DiscountType.AssignedToOrderTotal => !discount.OrderTotal.HasValue || order.TotalAmount >= discount.OrderTotal.Value,
                                    DiscountType.AssignedToProducts => discount.DiscountProducts.Any(p => p.ProductId == variant.ProductId),
                                    DiscountType.AssignedToCategories => variant.Product != null && variant.Product.ProductCategories.Any(pc => discount.DiscountCategories.Any(dc => dc.CategoryId == pc.CategoryId)),
                                    DiscountType.AssignedToManufacturers => variant.Product != null && variant.Product.ProductManufacturers.Any(pm => discount.DiscountManufacturers.Any(dm => dm.ManufacturerId == pm.ManufacturerId)),
                                    _ => true
                                };

                                if (isEligible)
                                {
                                    var itemTotal = item.UnitPrice * item.Quantity;
                                    eligibleItems.Add((item.ProductVariantId, itemTotal));
                                }
                            }

                            var eligibleSubtotal = eligibleItems.Sum(x => x.ItemTotal);
                            if (eligibleSubtotal > 0)
                            {
                                decimal discountAmount = 0;
                                if (discount.UsePercentage)
                                {
                                    discountAmount = eligibleSubtotal * discount.DiscountPercentage!.Value / 100;
                                }
                                else
                                {
                                    discountAmount = discount.DiscountAmount!.Value;
                                }

                                if (discount.MaximumDiscountAmount.HasValue && discountAmount > discount.MaximumDiscountAmount.Value)
                                {
                                    discountAmount = discount.MaximumDiscountAmount.Value;
                                }

                                if (discountAmount > eligibleSubtotal)
                                {
                                    discountAmount = eligibleSubtotal;
                                }

                                var eligibleTotal = eligibleSubtotal;
                                foreach (var (productVariantId, itemTotal) in eligibleItems)
                                {
                                    var proportion = itemTotal / eligibleTotal;
                                    var itemDiscount = Math.Round(discountAmount * proportion, 2);
                                    itemDiscounts[productVariantId] = itemDiscount;
                                }

                                var sumItemDiscounts = itemDiscounts.Values.Sum();
                                if (itemDiscounts.Any() && Math.Abs(sumItemDiscounts - discountAmount) > 0.01m)
                                {
                                    var diff = discountAmount - sumItemDiscounts;
                                    var firstKey = itemDiscounts.Keys.First();
                                    itemDiscounts[firstKey] = Math.Round(itemDiscounts[firstKey] + diff, 2);
                                }

                                totalOrderDiscount = discountAmount;
                            }

                        }
                    }
                }

                order.DiscountAmount = totalOrderDiscount;
                order.FinalPrice = order.TotalAmount - totalOrderDiscount;
                _commandRepository.Update(order);
                await _commandRepository.SaveChangesAsync();

                var orderItems = request.Items.Select(item =>
                {
                    var discountForItem = itemDiscounts.ContainsKey(item.ProductVariantId) ? itemDiscounts[item.ProductVariantId] : 0;
                    var itemTotal = item.UnitPrice * item.Quantity;
                    return new OrderItem()
                    {
                        OrderId = order.Id,
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = itemTotal - discountForItem,
                        TotalPriceTax = 0,
                        DiscountAmount = discountForItem,
                    };
                }).ToList();

                await _commandRepository.InsertRangeAsync(orderItems);
                await _commandRepository.SaveChangesAsync();

                foreach (var item in orderItems)
                {
                    var inventory = variants.FirstOrDefault(x => x.ProductInventory.VariantId == item.ProductVariantId)?.ProductInventory;

                    if (inventory != null)
                    {
                        var available = inventory.StockQuantity - inventory.ReservedQuantity;
                        if (available < item.Quantity)
                        {
                            result.Status = ResultStatusEnum.InsufficientStock;
                            var productVariant = variants.FirstOrDefault(x => x.Id == item.ProductVariantId);
                            result.Message = $"Insufficient stock for {productVariant.Product.Name} | {productVariant.SKU}";
                            await transaction.RollbackAsync();
                            return result;
                        }
                        if (request.PaymentMethodId == PaymentMethod.CashOnDelivery)
                        {
                            inventory.StockQuantity -= item.Quantity;
                        }
                        else
                        {
                            inventory.ReservedQuantity += item.Quantity;
                        }
                        _commandRepository.Update(inventory);

                        var inventoryTransaction = new ProductInventoryTransaction
                        {
                            ProductInventoryId = inventory.Id,
                            TransactionType = TransactionType.Sale,
                            StockQuantity = request.PaymentMethodId == PaymentMethod.CashOnDelivery ? -item.Quantity : 0,
                            ReservedQuantity = request.PaymentMethodId == PaymentMethod.CashOnDelivery ? 0 : item.Quantity,
                            CreatedDatetime = currentDatetime
                        };
                        await _commandRepository.InsertAsync(inventoryTransaction);
                    }
                    else
                    {
                        result.Status = ResultStatusEnum.Failed;
                        result.Message = $"Inventory for product variant doesn't exist {item.ProductVariantId}";
                        await transaction.RollbackAsync();
                        return result;
                    }
                }

                var payment = new Ecommerce.Core.Domain.Payment()
                {
                    OrderId = order.Id,
                    PaymentTypeId = request.PaymentMethodId,
                    Status = request.PaymentMethodId == PaymentMethod.CashOnDelivery ? PaymentStatus.Authorized : PaymentStatus.Pending,
                    CreatedOnUtc = currentDatetime
                };

                await _commandRepository.InsertAsync(payment);
                await _commandRepository.SaveChangesAsync();

                await transaction.CommitAsync();

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

        public async Task<Result> ConfirmOrder(int userId, int orderId)
        {
            var result = new Result();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.OrderItems)
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
                    result.Message = "Only pending orders can be confirmed";
                    return result;
                }

                using var transaction = await _commandRepository.BeginTransactionAsync();

                foreach (var item in order.OrderItems)
                {
                    var inventory = await _queryRepository.Table<ProductInventory>()
                        .FirstOrDefaultAsync(x => x.VariantId == item.ProductVariantId);

                    if (inventory == null)
                    {
                        result.Status = ResultStatusEnum.NotFound;
                        result.Message = $"Inventory not found for product variant {item.ProductVariantId}";
                        await transaction.RollbackAsync();
                        return result;
                    }

                    if (order.PaymentMethodId != PaymentMethod.CashOnDelivery)
                    {
                        var available = inventory.StockQuantity - inventory.ReservedQuantity;
                        if (available < item.Quantity)
                        {
                            result.Status = ResultStatusEnum.Failed;
                            result.Message = $"Insufficient stock for product variant {item.ProductVariantId}";
                            await transaction.RollbackAsync();
                            return result;
                        }

                        inventory.StockQuantity -= item.Quantity;
                        inventory.ReservedQuantity -= item.Quantity;
                        _commandRepository.Update(inventory);

                        var inventoryTransaction = new ProductInventoryTransaction
                        {
                            ProductInventoryId = inventory.Id,
                            TransactionType = TransactionType.Sale,
                            StockQuantity = -item.Quantity,
                            ReservedQuantity = -item.Quantity,
                            CreatedDatetime = DateTime.UtcNow
                        };
                        await _commandRepository.InsertAsync(inventoryTransaction);
                    }
                }

                order.OrderStatusId = OrderStatus.Processing;
                order.PaymentStatusId = PaymentStatus.Paid;
                _commandRepository.Update(order);

                var payment = await _queryRepository.Table<Ecommerce.Core.Domain.Payment>()
                    .FirstOrDefaultAsync(x => x.OrderId == orderId);
                if (payment != null)
                {
                    payment.Status = PaymentStatus.Paid;
                    payment.PaymentDateUtc = DateTime.UtcNow;
                    _commandRepository.Update(payment);
                }

                await _commandRepository.SaveChangesAsync();
                await transaction.CommitAsync();

                result.Message = "Order confirmed successfully";
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result> CancelOrder(int userId, int orderId)
        {
            var result = new Result();
            try
            {
                var order = await _queryRepository.Table<Ecommerce.Core.Domain.Order>()
                    .Include(x => x.OrderItems)
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

                using var transaction = await _commandRepository.BeginTransactionAsync();

                foreach (var item in order.OrderItems)
                {
                    var inventory = await _queryRepository.Table<ProductInventory>()
                        .FirstOrDefaultAsync(x => x.VariantId == item.ProductVariantId);

                    if (inventory != null)
                    {
                        inventory.ReservedQuantity -= item.Quantity;
                        if (inventory.ReservedQuantity < 0) inventory.ReservedQuantity = 0;
                        _commandRepository.Update(inventory);

                        var inventoryTransaction = new ProductInventoryTransaction
                        {
                            ProductInventoryId = inventory.Id,
                            TransactionType = TransactionType.Purchase,
                            StockQuantity = inventory.StockQuantity,
                            ReservedQuantity = inventory.ReservedQuantity,
                            CreatedDatetime = DateTime.UtcNow
                        };
                        await _commandRepository.InsertAsync(inventoryTransaction);
                    }
                }

                order.OrderStatusId = OrderStatus.Cancelled;
                _commandRepository.Update(order);

                await _commandRepository.SaveChangesAsync();
                await transaction.CommitAsync();

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