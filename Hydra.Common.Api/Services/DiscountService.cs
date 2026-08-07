using Hydra.Auth.Domain;
using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Kernel.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Hydra.Common.Api.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        IStringLocalizer<SharedResource> _sharedlocalizer;
        public DiscountService(IQueryRepository queryRepository, ICommandRepository commandRepository, IStringLocalizer<SharedResource> sharedlocalizer)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _sharedlocalizer = sharedlocalizer;
        }

        public async Task<Result<DiscountModel>> GetDiscountByCouponCode(int userId, string couponCode)
        {
            var result = new Result<DiscountModel>();
            var currentDate = DateTime.Now;
            var discount = await _queryRepository.Table<Discount>().FirstOrDefaultAsync(x => x.CouponCode == couponCode && x.IsActive);


            if (discount is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = _sharedlocalizer["DicountNotFound"];
                return result;
            }

            if (discount.StartDateUtc != null && discount.StartDateUtc > currentDate)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = _sharedlocalizer["DiscountDateExpired"];
                return result;
            }

            if (discount.EndDateUtc != null && discount.EndDateUtc < currentDate)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = _sharedlocalizer["DiscountDateExpired"];
                return result;
            }


            if (discount.DiscountLimitationId == DiscountLimitationType.NTimesOnly)
            {
                var limitationTimes = discount.LimitationTimes;
                var discountUsedTimes = _queryRepository.Table<OrderDiscount>().Where(x => x.DiscountId == discount.Id).Count();
                if (discountUsedTimes > discount.LimitationTimes)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = _sharedlocalizer["DiscountLimitExpired"];
                    return result;
                }
            }

            if (discount.DiscountLimitationId == DiscountLimitationType.NTimesPerCustomer)
            {
                var limitationTimes = discount.LimitationTimes;
                var discountUserCount = _queryRepository.Table<OrderDiscount>().Where(x => x.DiscountId == discount.Id && x.Order.UserId == userId).Count();
                if (discountUserCount > discount.LimitationTimes)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = _sharedlocalizer["DiscountLimitExpired"];
                    return result;
                }
            }
            var productIds = await _queryRepository.Table<DiscountProduct>()
                .Where(x => x.DiscountId == discount.Id)
                .Select(x => x.ProductId)
                .ToListAsync();

            var categoryIds = await _queryRepository.Table<DiscountCategory>()
                .Where(x => x.DiscountId == discount.Id)
                .Select(x => x.CategoryId)
                .ToListAsync();

            var manufacturerIds = await _queryRepository.Table<DiscountManufacturer>()
                .Where(x => x.DiscountId == discount.Id)
                .Select(x => x.ManufacturerId)
                .ToListAsync();

            var discountModel = new DiscountModel()
            {
                Id = discount.Id,
                Name = discount.Name,
                CouponCode = discount.CouponCode,
                AdminComment = discount.AdminComment,
                DiscountTypeId = discount.DiscountTypeId,
                UsePercentage = discount.UsePercentage,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountAmount = discount.DiscountAmount,
                MaximumDiscountAmount = discount.MaximumDiscountAmount,
                OrderTotal = discount.OrderTotal,
                StartDateUtc = discount.StartDateUtc,
                EndDateUtc = discount.EndDateUtc,
                RequiresCouponCode = discount.RequiresCouponCode,
                DiscountLimitationId = discount.DiscountLimitationId,
                LimitationTimes = discount.LimitationTimes,
                MaximumDiscountedQuantity = discount.MaximumDiscountedQuantity,
                IsActive = discount.IsActive,
                ProductIds = productIds,
                CategoryIds = categoryIds,
                ManufacturerIds = manufacturerIds
            };

            result.Data = discountModel;

            return result;
        }

        public async Task<Result<PaginatedList<DiscountModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<DiscountModel>>();

            var list = await (from discount in _queryRepository.Table<Discount>()
                              select new DiscountModel()
                              {
                                  Id = discount.Id,
                                  Name = discount.Name,
                                  CouponCode = discount.CouponCode,
                                  AdminComment = discount.AdminComment,
                                  DiscountTypeId = discount.DiscountTypeId,
                                  UsePercentage = discount.UsePercentage,
                                  DiscountPercentage = discount.DiscountPercentage,
                                  DiscountAmount = discount.DiscountAmount,
                                  MaximumDiscountAmount = discount.MaximumDiscountAmount,
                                  OrderTotal = discount.OrderTotal,
                                  StartDateUtc = discount.StartDateUtc,
                                  EndDateUtc = discount.EndDateUtc,
                                  RequiresCouponCode = discount.RequiresCouponCode,
                                  DiscountLimitationId = discount.DiscountLimitationId,
                                  LimitationTimes = discount.LimitationTimes,
                                  MaximumDiscountedQuantity = discount.MaximumDiscountedQuantity,
                                  IsActive = discount.IsActive

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        public async Task<Result<List<DiscountModel>>> GetListForSelect()
        {
            var result = new Result<List<DiscountModel>>();

            var list = await (from discount in _queryRepository.Table<Discount>()
                              select new DiscountModel()
                              {
                                  Id = discount.Id,
                                  Name = discount.Name + " | " + discount.CouponCode
                              }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;

            return result;
        }

        public async Task<Result<DiscountModel>> GetById(int id)
        {
            var result = new Result<DiscountModel>();
            var discount = await _queryRepository.Table<Discount>().FirstOrDefaultAsync(x => x.Id == id);
            if (discount is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Discount not found";
                return result;
            }

            var productIds = await _queryRepository.Table<DiscountProduct>()
                .Where(x => x.DiscountId == id)
                .Select(x => x.ProductId)
                .ToListAsync();

            var categoryIds = await _queryRepository.Table<DiscountCategory>()
                .Where(x => x.DiscountId == id)
                .Select(x => x.CategoryId)
                .ToListAsync();

            var manufacturerIds = await _queryRepository.Table<DiscountManufacturer>()
                .Where(x => x.DiscountId == id)
                .Select(x => x.ManufacturerId)
                .ToListAsync();

            var discountModel = new DiscountModel()
            {
                Id = discount.Id,
                Name = discount.Name,
                CouponCode = discount.CouponCode,
                AdminComment = discount.AdminComment,
                DiscountTypeId = discount.DiscountTypeId,
                UsePercentage = discount.UsePercentage,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountAmount = discount.DiscountAmount,
                MaximumDiscountAmount = discount.MaximumDiscountAmount,
                OrderTotal = discount.OrderTotal,
                StartDateUtc = discount.StartDateUtc,
                EndDateUtc = discount.EndDateUtc,
                RequiresCouponCode = discount.RequiresCouponCode,
                DiscountLimitationId = discount.DiscountLimitationId,
                LimitationTimes = discount.LimitationTimes,
                MaximumDiscountedQuantity = discount.MaximumDiscountedQuantity,
                IsActive = discount.IsActive,
                ProductIds = productIds,
                CategoryIds = categoryIds,
                ManufacturerIds = manufacturerIds
            };

            result.Data = discountModel;

            return result;
        }

        public async Task<Result<DiscountModel>> Add(DiscountModel discountModel)
        {
            var result = new Result<DiscountModel>();

            var validationErrors = ValidateDiscountModel(discountModel);
            if (validationErrors.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Errors = validationErrors;
                return result;
            }

            if (discountModel.RequiresCouponCode && string.IsNullOrWhiteSpace(discountModel.CouponCode))
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Errors.Add(new Error(nameof(discountModel.CouponCode), "Coupon code is required when RequiresCouponCode is enabled."));
                return result;
            }

            if (discountModel.RequiresCouponCode)
            {
                bool isExist = await _queryRepository.Table<Discount>().AnyAsync(x => x.CouponCode == discountModel.CouponCode);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The coupon code already exists.";
                    result.Errors.Add(new Error(nameof(discountModel.CouponCode), "The coupon code already exists."));
                    return result;
                }
            }

            await using var transaction = await _commandRepository.BeginTransactionAsync();
            try
            {
                var discount = new Discount()
                {
                    Name = discountModel.Name,
                    CouponCode = discountModel.CouponCode,
                    AdminComment = discountModel.AdminComment,
                    DiscountTypeId = discountModel.DiscountTypeId,
                    UsePercentage = discountModel.UsePercentage,
                    MaximumDiscountAmount = discountModel.MaximumDiscountAmount,
                    StartDateUtc = discountModel.StartDateUtc,
                    EndDateUtc = discountModel.EndDateUtc,
                    RequiresCouponCode = discountModel.RequiresCouponCode,
                    DiscountLimitationId = discountModel.DiscountLimitationId,
                    MaximumDiscountedQuantity = discountModel.DiscountLimitationId != DiscountLimitationType.Unlimited ? discountModel.MaximumDiscountedQuantity : 0,
                    IsActive = discountModel.IsActive
                };
                if (discountModel.DiscountLimitationId == DiscountLimitationType.NTimesOnly || discountModel.DiscountLimitationId == DiscountLimitationType.NTimesPerCustomer)
                {
                    discount.LimitationTimes = discountModel.LimitationTimes;
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToOrderTotal)
                {
                    discount.OrderTotal = discountModel.OrderTotal;
                }
                if (discountModel.UsePercentage)
                {
                    discount.DiscountPercentage = discountModel.DiscountPercentage;
                }
                else
                {
                    discount.DiscountAmount = discountModel.DiscountAmount;
                }

                await _commandRepository.InsertAsync(discount);
                await _commandRepository.SaveChangesAsync();

                var discountId = discount.Id;

                if (discountModel.DiscountTypeId == DiscountType.AssignedToProducts && discountModel.ProductIds.Any())
                {
                    var discountProducts = discountModel.ProductIds.Select(pid => new DiscountProduct()
                    {
                        DiscountId = discountId,
                        ProductId = pid
                    }).ToList();
                    await _commandRepository.InsertRangeAsync(discountProducts);
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToCategories && discountModel.CategoryIds.Any())
                {
                    var discountCategories = discountModel.CategoryIds.Select(cid => new DiscountCategory()
                    {
                        DiscountId = discountId,
                        CategoryId = cid
                    }).ToList();
                    await _commandRepository.InsertRangeAsync(discountCategories);
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToManufacturers && discountModel.ManufacturerIds.Any())
                {
                    var discountManufacturers = discountModel.ManufacturerIds.Select(mid => new DiscountManufacturer()
                    {
                        DiscountId = discountId,
                        ManufacturerId = mid
                    }).ToList();
                    await _commandRepository.InsertRangeAsync(discountManufacturers);
                }

                await _commandRepository.SaveChangesAsync();
                await transaction.CommitAsync();

                discountModel.Id = discount.Id;
                result.Data = discountModel;

                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<DiscountModel>> Update(DiscountModel discountModel)
        {
            var result = new Result<DiscountModel>();

            var validationErrors = ValidateDiscountModel(discountModel);
            if (validationErrors.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Errors = validationErrors;
                return result;
            }

            if (discountModel.RequiresCouponCode && string.IsNullOrWhiteSpace(discountModel.CouponCode))
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Errors.Add(new Error(nameof(discountModel.CouponCode), "Coupon code is required when RequiresCouponCode is enabled."));
                return result;
            }

            if (discountModel.RequiresCouponCode)
            {
                bool isExist = await _queryRepository.Table<Discount>().AnyAsync(x => x.Id != discountModel.Id && x.CouponCode == discountModel.CouponCode);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The coupon code already exists.";
                    result.Errors.Add(new Error(nameof(discountModel.CouponCode), "The coupon code already exists."));
                    return result;
                }
            }

            await using var transaction = await _commandRepository.BeginTransactionAsync();
            try
            {
                var discount = await _queryRepository.Table<Discount>().FirstAsync(x => x.Id == discountModel.Id);
                if (discount is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Discount not found";
                    return result;
                }

                var oldDiscount = discount.DiscountTypeId;

                if (discountModel.DiscountLimitationId == DiscountLimitationType.NTimesOnly || discountModel.DiscountLimitationId == DiscountLimitationType.NTimesPerCustomer)
                {
                    discount.LimitationTimes = discountModel.LimitationTimes;
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToOrderTotal)
                {
                    discount.OrderTotal = discountModel.OrderTotal;
                    discount.DiscountPercentage = 0;
                }
                if (discountModel.UsePercentage)
                {
                    discount.DiscountPercentage = discountModel.DiscountPercentage;
                }
                else
                {
                    discount.DiscountAmount = discountModel.DiscountAmount;
                }

                discount.MaximumDiscountedQuantity = discountModel.DiscountLimitationId != DiscountLimitationType.Unlimited ? discountModel.MaximumDiscountedQuantity : 0;

                discount.Name = discountModel.Name;
                discount.CouponCode = discountModel.CouponCode;
                discount.AdminComment = discountModel.AdminComment;
                discount.DiscountTypeId = discountModel.DiscountTypeId;
                discount.UsePercentage = discountModel.UsePercentage;
                discount.MaximumDiscountAmount = discountModel.MaximumDiscountAmount;
                discount.StartDateUtc = discountModel.StartDateUtc;
                discount.EndDateUtc = discountModel.EndDateUtc;
                discount.RequiresCouponCode = discountModel.RequiresCouponCode;
                discount.DiscountLimitationId = discountModel.DiscountLimitationId;
                discount.MaximumDiscountedQuantity = discountModel.MaximumDiscountedQuantity;
                discount.IsActive = discountModel.IsActive;

                _commandRepository.Update(discount);

                if (oldDiscount != discountModel.DiscountTypeId)
                {
                    if (oldDiscount == DiscountType.AssignedToProducts)
                    {
                        var oldItems = _queryRepository.Table<DiscountProduct>().Where(x => x.DiscountId == discountModel.Id).ToList();
                        foreach (var item in oldItems)
                            _commandRepository.Delete(item);
                    }
                    if (oldDiscount == DiscountType.AssignedToCategories)
                    {
                        var oldItems = _queryRepository.Table<DiscountCategory>().Where(x => x.DiscountId == discountModel.Id).ToList();
                        foreach (var item in oldItems)
                            _commandRepository.Delete(item);
                    }
                    if (oldDiscount == DiscountType.AssignedToManufacturers)
                    {
                        var oldItems = _queryRepository.Table<DiscountManufacturer>().Where(x => x.DiscountId == discountModel.Id).ToList();
                        foreach (var item in oldItems)
                            _commandRepository.Delete(item);
                    }
                    _commandRepository.SaveChanges();
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToProducts)
                {
                    await UpdateDiscountProduct(discount.Id, discountModel.ProductIds.ToArray());
                }

                if (discountModel.DiscountTypeId == DiscountType.AssignedToCategories)
                    await UpdateDiscountCategory(discount.Id, discountModel.CategoryIds.ToArray());

                if (discountModel.DiscountTypeId == DiscountType.AssignedToManufacturers)
                    await UpdateDiscountManufacturer(discount.Id, discountModel.ManufacturerIds.ToArray());

                _commandRepository.SaveChanges();
                await transaction.CommitAsync();

                result.Data = discountModel;

                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        private async Task<Result> UpdateDiscountProduct(int discountId, int[] newProductIds)
        {
            var result = new Result();
            try
            {
                var discountProducts = _queryRepository.Table<DiscountProduct>().AsNoTracking().Where(x => x.DiscountId == discountId).ToList();
                var currentProductIds = discountProducts.Select(x => x.ProductId).ToArray();

                if (!newProductIds.SequenceEqual(currentProductIds))
                {
                    foreach (var dp in discountProducts)
                        _commandRepository.Delete(dp);

                    foreach (var pid in newProductIds)
                        await _commandRepository.InsertAsync(new DiscountProduct() { DiscountId = discountId, ProductId = pid });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateDiscountCategory(int discountId, int[] newCategoryIds)
        {
            var result = new Result();
            try
            {
                var discountCategories = _queryRepository.Table<DiscountCategory>().AsNoTracking().Where(x => x.DiscountId == discountId).ToList();
                var currentCategoryIds = discountCategories.Select(x => x.CategoryId).ToArray();

                if (!newCategoryIds.SequenceEqual(currentCategoryIds))
                {
                    foreach (var dc in discountCategories)
                        _commandRepository.Delete(dc);

                    foreach (var cid in newCategoryIds)
                        await _commandRepository.InsertAsync(new DiscountCategory() { DiscountId = discountId, CategoryId = cid });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private async Task<Result> UpdateDiscountManufacturer(int discountId, int[] newManufacturerIds)
        {
            var result = new Result();
            try
            {
                var discountManufacturers = _queryRepository.Table<DiscountManufacturer>().AsNoTracking().Where(x => x.DiscountId == discountId).ToList();
                var currentManufacturerIds = discountManufacturers.Select(x => x.ManufacturerId).ToArray();

                if (!newManufacturerIds.SequenceEqual(currentManufacturerIds))
                {
                    foreach (var dm in discountManufacturers)
                        _commandRepository.Delete(dm);

                    foreach (var mid in newManufacturerIds)
                        await _commandRepository.InsertAsync(new DiscountManufacturer() { DiscountId = discountId, ManufacturerId = mid });
                }
                return result;
            }
            catch (Exception e)
            {
                result.Status = ResultStatusEnum.ExceptionThrowed;
                result.Message = e.Message;
                return result;
            }
        }

        private List<Error> ValidateDiscountModel(DiscountModel model)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(model.Name))
                errors.Add(new Error(nameof(model.Name), "Name is required."));
            if (model.UsePercentage)
            {
                if (model.DiscountPercentage < 0)
                    errors.Add(new Error(nameof(model.DiscountPercentage), "Discount percentage cannot be negative."));

                if (model.DiscountPercentage > 100)
                    errors.Add(new Error(nameof(model.DiscountPercentage), "Discount percentage cannot be greater than 100."));
            }
            else
            {
                if (model.DiscountAmount < 0)
                    errors.Add(new Error(nameof(model.DiscountAmount), "Discount amount cannot be negative."));
            }
            if (model.MaximumDiscountAmount.HasValue && model.MaximumDiscountAmount < 0)
                errors.Add(new Error(nameof(model.MaximumDiscountAmount), "Maximum discount amount cannot be negative."));

            if (model.StartDateUtc.HasValue && model.EndDateUtc.HasValue && model.EndDateUtc < model.StartDateUtc)
                errors.Add(new Error(nameof(model.EndDateUtc), "End date cannot be earlier than start date."));

            if (model.LimitationTimes < 0)
                errors.Add(new Error(nameof(model.LimitationTimes), "Limitation times cannot be negative."));

            if (model.MaximumDiscountedQuantity.HasValue && model.MaximumDiscountedQuantity < 0)
                errors.Add(new Error(nameof(model.MaximumDiscountedQuantity), "Maximum discounted quantity cannot be negative."));

            if (model.DiscountLimitationId == DiscountLimitationType.NTimesOnly && model.LimitationTimes <= 0)
                errors.Add(new Error(nameof(model.LimitationTimes), "Limitation times must be greater than zero when limitation type is N Times Only."));

            if (model.DiscountLimitationId == DiscountLimitationType.NTimesPerCustomer && model.LimitationTimes <= 0)
                errors.Add(new Error(nameof(model.LimitationTimes), "Limitation times must be greater than zero when limitation type is N Times Per Customer."));

            switch (model.DiscountTypeId)
            {
                case DiscountType.AssignedToCouponCode:
                    if (!model.RequiresCouponCode)
                        errors.Add(new Error(nameof(model.RequiresCouponCode), "RequiresCouponCode must be enabled when discount type is AssignedToCouponCode."));
                    if (string.IsNullOrWhiteSpace(model.CouponCode))
                        errors.Add(new Error(nameof(model.CouponCode), "Coupon code is required when discount type is AssignedToCouponCode."));
                    break;

                case DiscountType.AssignedToOrderTotal:
                    if (!model.OrderTotal.HasValue)
                        errors.Add(new Error(nameof(model.OrderTotal), "Order total is required when discount type is AssignedToOrderTotal."));
                    break;

                case DiscountType.AssignedToProducts:
                    if (!model.ProductIds.Any())
                        errors.Add(new Error(nameof(model.ProductIds), "At least one product is required when discount type is AssignedToProduct."));
                    break;

                case DiscountType.AssignedToCategories:
                    if (!model.CategoryIds.Any())
                        errors.Add(new Error(nameof(model.CategoryIds), "At least one category is required when discount type is AssignedToCategory."));
                    break;

                case DiscountType.AssignedToManufacturers:
                    if (!model.ManufacturerIds.Any())
                        errors.Add(new Error(nameof(model.ManufacturerIds), "At least one manufacturer is required when discount type is AssignedToManufacturer."));
                    break;
            }

            return errors;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var discount = await _queryRepository.Table<Discount>().FirstOrDefaultAsync(x => x.Id == id);
            if (discount is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Discount not found";
                return result;
            }

            _commandRepository.Delete(discount);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}
