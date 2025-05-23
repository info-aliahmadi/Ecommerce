﻿using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic.Core;
using Hydra.Kernel.Extension;
using Hydra.Ecommerce.Core.Domain;

namespace Hydra.Product.Api.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public ProductAttributeService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private List<ProductAttributeModel> GetProductAttributesList()
        {

            var list = (from productAttribute in _queryRepository.Table<ProductAttribute>()
                        select new ProductAttributeModel()
                        {
                            Id = productAttribute.Id,
                            Name = productAttribute.Name,
                            Value = productAttribute.Value,
                            AttributeType = productAttribute.AttributeType,
                            Description = productAttribute.Description,
                            DisplayOrder = productAttribute.DisplayOrder,
                            PictureId = productAttribute.PictureId,

                        }).OrderBy(x => x.DisplayOrder).Cacheable().ToList();

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Result<List<ProductAttributeModel>> GetList()
        {
            var result = new Result<List<ProductAttributeModel>>();

            var list = GetProductAttributesList();

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Result<List<ProductAttributeModel>> GetListForSelect()
        {
            var result = new Result<List<ProductAttributeModel>>();

            result.Data = GetProductAttributesList();

            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<ProductAttributeModel> GetById(int id)
        {
            var result = new Result<ProductAttributeModel>();

            result.Data = GetProductAttributesList().FirstOrDefault(x => x.Id == id) ?? new ProductAttributeModel();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productAttributeModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductAttributeModel>> Add(ProductAttributeModel productAttributeModel)
        {
            var result = new Result<ProductAttributeModel>();
            try
            {
                var isExist = await _queryRepository.Table<ProductAttribute>().AnyAsync(x => x.Id != productAttributeModel.Id && x.Name == productAttributeModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exist";
                    result.Errors.Add(new Error(nameof(productAttributeModel.Id), "The Name already exist"));
                    return result;
                }
                var date = DateTime.UtcNow;
                var productAttribute = new ProductAttribute()
                {
                    Id = productAttributeModel.Id,
                    Name = productAttributeModel.Name,
                    Value = productAttributeModel.Value,
                    AttributeType = productAttributeModel.AttributeType,
                    Description = productAttributeModel.Description,
                    DisplayOrder = productAttributeModel.DisplayOrder,
                    PictureId = productAttributeModel.PictureId

                };

                await _commandRepository.InsertAsync(productAttribute);
                await _commandRepository.SaveChangesAsync();

                productAttributeModel.Id = productAttribute.Id;

                result.Data = productAttributeModel;

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
        /// <param name="productAttributeModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductAttributeModel>> Update(ProductAttributeModel productAttributeModel)
        {
            var result = new Result<ProductAttributeModel>();
            try
            {
                var productAttribute = await _queryRepository.Table<ProductAttribute>().FirstOrDefaultAsync(x => x.Id == productAttributeModel.Id);
                if (productAttribute is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Product Attribute not found";
                    return result;
                }
                var isExist = await _queryRepository.Table<ProductAttribute>().AnyAsync(x => x.Id != productAttributeModel.Id && x.Name == productAttribute.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exist";
                    result.Errors.Add(new Error(nameof(productAttributeModel.Id), "The Name already exist"));
                    return result;
                }

                productAttribute.Name = productAttributeModel.Name;
                productAttribute.Value = productAttributeModel.Value;
                productAttribute.AttributeType = productAttributeModel.AttributeType;
                productAttribute.Description = productAttributeModel.Description;
                productAttribute.DisplayOrder = productAttributeModel.DisplayOrder;
                productAttribute.PictureId = productAttributeModel.PictureId;

                _commandRepository.UpdateAsync(productAttribute);
                await _commandRepository.SaveChangesAsync();

                result.Data = productAttributeModel;

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
            var productAttribute = await _queryRepository.Table<ProductAttribute>()
                .FirstOrDefaultAsync(x => x.Id == id);

            _commandRepository.DeleteAsync(productAttribute);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}