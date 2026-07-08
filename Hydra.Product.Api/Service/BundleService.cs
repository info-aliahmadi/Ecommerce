using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Hydra.Product.Api.Services
{
    public class BundleService : IBundleService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;

        public BundleService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        public async Task<Result<List<BundleDisplayModel>>> GetPublishedBundles()
        {
            var result = new Result<List<BundleDisplayModel>>();

            var list = await (from bundle in _queryRepository.Table<Bundle>()
                              where bundle.ShowOnHomepage
                              select new BundleDisplayModel()
                              {
                                  Id = bundle.Id,
                                  Name = bundle.Name,
                                  Description = bundle.Description,
                                  DisplayOrder = bundle.DisplayOrder,
                                  ShowOnHomepage = bundle.ShowOnHomepage,
                                  ProductsCount = bundle.ProductBundles.Count(),
                                  ProductIds = bundle.ProductBundles.Select(pb => new ProductBundleModel()
                                  {
                                      ProductId = pb.ProductId,
                                      DisplayOrder = pb.DisplayOrder
                                  }).ToList()
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

        public async Task<Result<List<BundleModel>>> GetBundlesList()
        {
            var result = new Result<List<BundleModel>>();

            var list = await (from bundle in _queryRepository.Table<Bundle>()
                              select new BundleModel()
                              {
                                  Id = bundle.Id,
                                  Name = bundle.Name,
                                  Description = bundle.Description,
                                  DisplayOrder = bundle.DisplayOrder,
                                  ShowOnHomepage = bundle.ShowOnHomepage,
                                  CreatedOnUtc = bundle.CreatedOnUtc,
                                  ProductIds = bundle.ProductBundles.Select(pb => new ProductBundleModel()
                                  {
                                      ProductId = pb.ProductId,
                                      DisplayOrder = pb.DisplayOrder
                                  }).ToList()
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

        public async Task<Result<List<BundleModel>>> GetListForSelect()
        {
            return await GetBundlesList();
        }

        public async Task<Result<BundleModel>> GetById(int id)
        {
            var result = new Result<BundleModel>();
            var bundle = await _queryRepository.Table<Bundle>()
                .Include(x => x.ProductBundles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bundle is null)
            {
                result.Data = new BundleModel();
                return result;
            }

            result.Data = new BundleModel()
            {
                Id = bundle.Id,
                Name = bundle.Name,
                Description = bundle.Description,
                DisplayOrder = bundle.DisplayOrder,
                ShowOnHomepage = bundle.ShowOnHomepage,
                CreatedOnUtc = bundle.CreatedOnUtc,
                ProductIds = bundle.ProductBundles.Select(pb => new ProductBundleModel()
                {
                    ProductId = pb.ProductId,
                    DisplayOrder = pb.DisplayOrder
                }).ToList()
            };
            return result;
        }

        public async Task<Result<BundleModel>> Add(BundleModel bundleModel)
        {
            var result = new Result<BundleModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Bundle>().AnyAsync(x => x.Name == bundleModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(bundleModel.Id), "The Name already exists"));
                    return result;
                }

                var date = DateTime.UtcNow;
                var bundle = new Bundle()
                {
                    Name = bundleModel.Name,
                    Description = bundleModel.Description,
                    DisplayOrder = bundleModel.DisplayOrder,
                    ShowOnHomepage = bundleModel.ShowOnHomepage,
                    CreatedOnUtc = date
                };

                await _commandRepository.InsertAsync(bundle);
                await _commandRepository.SaveChangesAsync();

                if (bundleModel.ProductIds?.Any() == true)
                {
                    foreach (var pb in bundleModel.ProductIds)
                    {
                        var productBundle = new ProductBundle()
                        {
                            BundleId = bundle.Id,
                            ProductId = pb.ProductId,
                            DisplayOrder = pb.DisplayOrder
                        };
                        await _commandRepository.InsertAsync(productBundle);
                    }
                    await _commandRepository.SaveChangesAsync();
                }

                bundleModel.Id = bundle.Id;
                result.Data = bundleModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<BundleModel>> Update(BundleModel bundleModel)
        {
            var result = new Result<BundleModel>();
            try
            {
                var bundle = await _queryRepository.Table<Bundle>()
                    .Include(x => x.ProductBundles)
                    .FirstOrDefaultAsync(x => x.Id == bundleModel.Id);
                if (bundle is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Bundle not found";
                    return result;
                }

                var isExist = await _queryRepository.Table<Bundle>().AnyAsync(x => x.Id != bundleModel.Id && x.Name == bundleModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(bundleModel.Id), "The Name already exists"));
                    return result;
                }

                bundle.Name = bundleModel.Name;
                bundle.Description = bundleModel.Description;
                bundle.DisplayOrder = bundleModel.DisplayOrder;
                bundle.ShowOnHomepage = bundleModel.ShowOnHomepage;

                _commandRepository.UpdateAsync(bundle);

                // Sync ProductBundles: remove existing, add new ones
                if (bundle.ProductBundles?.Any() == true)
                {
                    foreach (var existing in bundle.ProductBundles)
                    {
                        _commandRepository.DeleteAsync(existing);
                    }
                }

                if (bundleModel.ProductIds?.Any() == true)
                {
                    foreach (var pb in bundleModel.ProductIds)
                    {
                        var productBundle = new ProductBundle()
                        {
                            BundleId = bundle.Id,
                            ProductId = pb.ProductId,
                            DisplayOrder = pb.DisplayOrder
                        };
                        await _commandRepository.InsertAsync(productBundle);
                    }
                }

                await _commandRepository.SaveChangesAsync();

                result.Data = bundleModel;
                return result;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = ResultStatusEnum.ExceptionThrowed;
                return result;
            }
        }

        public async Task<Result<List<BundleModel>>> UpdateOrder(List<BundleModel> linkModelList)
        {
            var result = new Result<List<BundleModel>>();

            var ids = linkModelList.Select(x => x.Id).ToArray();
            var i = linkModelList.Count;
            foreach (var item in linkModelList)
            {
                item.DisplayOrder = i--;
            }

            var visibleList = _queryRepository.Table<Bundle>().Where(x => ids.Contains(x.Id)).ToList();
            foreach (var item in visibleList)
            {
                var model = linkModelList.First(x => x.Id == item.Id);
                item.DisplayOrder = model.DisplayOrder;
                _commandRepository.UpdateAsync(item);
            }

            await _commandRepository.SaveChangesAsync();

            result.Data = linkModelList;
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var bundle = await _queryRepository.Table<Bundle>()
                .Include(x => x.ProductBundles)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bundle is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Bundle not found";
                return result;
            }

            if (bundle.ProductBundles.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Message = "The Bundle has associated products and cannot be deleted";
                return result;
            }

            _commandRepository.DeleteAsync(bundle);
            await _commandRepository.SaveChangesAsync();

            return result;
        }
    }
}
