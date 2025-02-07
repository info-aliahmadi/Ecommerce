using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Hydra.Kernel.Extension;
using Hydra.Inventory.Core.Models;
using Hydra.Inventory.Core.Interfaces;

namespace Hydra.Product.Api.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public InventoryService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<InventoryModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<InventoryModel>>();

            var list = await (from productInventory in _queryRepository.Table<ProductInventory>()
                              select new InventoryModel()
                              {
                                  Id = productInventory.Id,
                                  ProductId = productInventory.ProductId,
                                  StockQuantity = productInventory.StockQuantity,
                                  ReservedQuantity = productInventory.ReservedQuantity,

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<InventoryModel>> GetById(int id)
        {
            var result = new Result<InventoryModel>();
            //var productInventory = await _queryRepository.Table<ProductInventory>().FirstOrDefaultAsync(x => x.Id == id);

            //var inventoryModel = new InventoryModel()
            //{
            //    Id = productInventory.Id,
            //    ProductId = productInventory.ProductId,
            //    StockQuantity = productInventory.StockQuantity,
            //    ReservedQuantity = productInventory.ReservedQuantity,

            //};
            //result.Data = inventoryModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        public async Task<Result<InventoryModel>> Add(InventoryModel inventoryModel)
        {
            var result = new Result<InventoryModel>();
            try
            {
                bool isExist = await _queryRepository.Table<ProductInventory>().AnyAsync(x => x.Id == inventoryModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(inventoryModel.Id), "The Id already exist"));
                    return result;
                }
                var productInventory = new ProductInventory()
                {
                    ProductId = inventoryModel.ProductId,
                    StockQuantity = inventoryModel.StockQuantity,
                    ReservedQuantity = inventoryModel.ReservedQuantity,

                };

                await _commandRepository.InsertAsync(productInventory);
                await _commandRepository.SaveChangesAsync();

                inventoryModel.Id = productInventory.Id;

                result.Data = inventoryModel;

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
        /// <param name="inventoryModel"></param>
        /// <returns></returns>
        public async Task<Result<InventoryModel>> Update(InventoryModel inventoryModel)
        {
            var result = new Result<InventoryModel>();
            try
            {
                var productInventory = await _queryRepository.Table<ProductInventory>().FirstAsync(x => x.Id == inventoryModel.Id);
                if (productInventory is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The ProductInventory not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<ProductInventory>().AnyAsync(x => x.Id != inventoryModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(inventoryModel.Id), "The Id already exist"));
                    return result;
                }
                productInventory.ProductId = inventoryModel.ProductId;
                productInventory.StockQuantity = inventoryModel.StockQuantity;
                productInventory.ReservedQuantity = inventoryModel.ReservedQuantity;

                _commandRepository.UpdateAsync(productInventory);
                await _commandRepository.SaveChangesAsync();

                result.Data = inventoryModel;

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
            var productInventory = await _queryRepository.Table<ProductInventory>().FirstOrDefaultAsync(x => x.Id == id);
            if (productInventory is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The ProductInventory not found";
                return result;
            }

            _commandRepository.DeleteAsync(productInventory);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}