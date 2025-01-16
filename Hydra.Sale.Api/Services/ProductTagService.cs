﻿using Hydra.Infrastructure.GeneralModels;
using Hydra.Infrastructure.Data.Interface;
using Hydra.Sale.Core.Domain;
using Hydra.Sale.Core.Interfaces;
using Hydra.Sale.Core.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Infrastructure.Data.Extension;

namespace Hydra.Sale.Api.Services
{
    public class ProductTagService : IProductTagService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public ProductTagService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<ProductTagModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<ProductTagModel>>();

            var list = await (from productTag in _queryRepository.Table<ProductTag>()
                              select new ProductTagModel()
                              {
                                  Id = productTag.Id,
                                  Name = productTag.Name,
                                  //Products = productTag.Products,

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<List<ProductTagModel>>> GetListForSelect()
        {
            var result = new Result<List<ProductTagModel>>();

            var list = await (from productTag in _queryRepository.Table<ProductTag>()
                              select new ProductTagModel()
                              {
                                  Id = productTag.Id,
                                  Name = productTag.Name,
                                  //Products = productTag.Products,

                              }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<ProductTagModel>> GetById(int id)
        {
            var result = new Result<ProductTagModel>();
            var productTag = await _queryRepository.Table<ProductTag>().FirstOrDefaultAsync(x => x.Id == id);

            var productTagModel = new ProductTagModel()
            {
                Id = productTag.Id,
                Name = productTag.Name,
                //Products = productTag.Products,

            };
            result.Data = productTagModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductTagModel>> Add(ProductTagModel productTagModel)
        {
            var result = new Result<ProductTagModel>();
            try
            {
                bool isExist = await _queryRepository.Table<ProductTag>().AnyAsync(x => x.Id == productTagModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(productTagModel.Id), "The Id already exist"));
                    return result;
                }
                var productTag = new ProductTag()
                {
                    Name = productTagModel.Name,
                    //Products = productTagModel.Products,

                };

                await _commandRepository.InsertAsync(productTag);
                await _commandRepository.SaveChangesAsync();

                productTagModel.Id = productTag.Id;

                result.Data = productTagModel;

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
        /// <param name="tagModel"></param>
        /// <returns></returns>
        public async Task<Result<List<ProductTagModel>>> Add(string[] tags)
        {
            var result = new Result<List<ProductTagModel>>();

            var existedTags = _queryRepository.Table<ProductTag>().AsNoTracking().Where(x => tags.Contains(x.Name)).ToList();

            var newTags = tags.Where(x => !existedTags.Select(s => s.Name).ToArray().Contains(x)).Select(tagName => new ProductTag()
            {
                Name = tagName
            });

            foreach (var tag in newTags)
            {
                await _commandRepository.InsertAsync(tag);
            }
            await _commandRepository.SaveChangesAsync();

            _commandRepository.ResetContextState();

            var allTags = _queryRepository.Table<ProductTag>().AsNoTracking().Where(x => tags.Contains(x.Name)).Select(x=>new ProductTagModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            result.Data = allTags;

            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="productTagModel"></param>
        /// <returns></returns>
        public async Task<Result<ProductTagModel>> Update(ProductTagModel productTagModel)
        {
            var result = new Result<ProductTagModel>();
            try
            {
                var productTag = await _queryRepository.Table<ProductTag>().FirstAsync(x => x.Id == productTagModel.Id);
                if (productTag is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The ProductTag not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<ProductTag>().AnyAsync(x => x.Id != productTagModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(productTagModel.Id), "The Id already exist"));
                    return result;
                }
                productTag.Name = productTagModel.Name;
                //productTag.Products = productTagModel.Products;

                _commandRepository.UpdateAsync(productTag);
                await _commandRepository.SaveChangesAsync();

                result.Data = productTagModel;

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
            var productTag = await _queryRepository.Table<ProductTag>().FirstOrDefaultAsync(x => x.Id == id);
            if (productTag is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The ProductTag not found";
                return result;
            }

            _commandRepository.DeleteAsync(productTag);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}