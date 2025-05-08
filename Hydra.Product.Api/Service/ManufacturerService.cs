﻿using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Hydra.Kernel.Extension;

namespace Hydra.Product.Api.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public ManufacturerService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<List<ManufacturerModel>>> GetManufacturersList()
        {
            var result = new Result<List<ManufacturerModel>>();

            var list = await (from manufacturer in _queryRepository.Table<Manufacturer>()
                              select new ManufacturerModel()
                              {
                                  Id = manufacturer.Id,
                                  Name = manufacturer.Name,
                                  MetaKeywords = manufacturer.MetaKeywords,
                                  MetaTitle = manufacturer.MetaTitle,
                                  Description = manufacturer.Description,
                                  MetaDescription = manufacturer.MetaDescription,
                                  PictureId = manufacturer.PictureId,
                                  Published = manufacturer.Published,
                                  Deleted = manufacturer.Deleted,
                                  DisplayOrder = manufacturer.DisplayOrder,
                                  CreatedOnUtc = manufacturer.CreatedOnUtc,
                                  UpdatedOnUtc = manufacturer.UpdatedOnUtc

                              }).OrderByDescending(x => x.Id).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }


        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<ManufacturerModel>>> GetListForSelect()
        {
            var result = new Result<List<ManufacturerModel>>();

            var list = await GetManufacturersList();
            result.Data = list.Data;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<ManufacturerModel> GetById(int id)
        {
            var result = new Result<ManufacturerModel>();

            result.Data = GetManufacturersList().Result.Data.FirstOrDefault(x => x.Id == id) ?? new ManufacturerModel();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        public async Task<Result<ManufacturerModel>> Add(ManufacturerModel manufacturerModel)
        {
            var result = new Result<ManufacturerModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Manufacturer>().AnyAsync(x => x.Id == manufacturerModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(manufacturerModel.Id), "The Id already exist"));
                    return result;
                }

                var date = DateTime.UtcNow;
                var manufacturer = new Manufacturer()
                {
                    Name = manufacturerModel.Name,
                    MetaKeywords = manufacturerModel.MetaKeywords,
                    MetaTitle = manufacturerModel.MetaTitle,
                    Description = manufacturerModel.Description,
                    MetaDescription = manufacturerModel.MetaDescription,
                    PictureId = manufacturerModel.PictureId,
                    Published = manufacturerModel.Published,
                    Deleted = false,
                    DisplayOrder = manufacturerModel.DisplayOrder,
                    CreatedOnUtc = date,
                    UpdatedOnUtc = date
                    //ProductManufacturers = manufacturerModel.ProductManufacturers,
                    //Discounts = manufacturerModel.Discounts,

                };

                await _commandRepository.InsertAsync(manufacturer);
                await _commandRepository.SaveChangesAsync();

                manufacturerModel.Id = manufacturer.Id;

                result.Data = manufacturerModel;

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
        /// <param name="manufacturerModel"></param>
        /// <returns></returns>
        public async Task<Result<ManufacturerModel>> Update(ManufacturerModel manufacturerModel)
        {
            var result = new Result<ManufacturerModel>();
            try
            {
                var manufacturer = await _queryRepository.Table<Manufacturer>().FirstOrDefaultAsync(x => x.Id == manufacturerModel.Id);
                if (manufacturer is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Manufacturer not found";
                    return result;
                }
                var isExist = await _queryRepository.Table<Manufacturer>().AnyAsync(x => x.Id != manufacturerModel.Id && x.Name == manufacturer.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(manufacturerModel.Id), "The Id already exist"));
                    return result;
                }

                manufacturer.Name = manufacturerModel.Name;
                manufacturer.MetaKeywords = manufacturerModel.MetaKeywords;
                manufacturer.MetaTitle = manufacturerModel.MetaTitle;
                manufacturer.Description = manufacturerModel.Description;
                manufacturer.MetaDescription = manufacturerModel.MetaDescription;
                manufacturer.PictureId = manufacturerModel.PictureId;
                manufacturer.Published = manufacturerModel.Published;
                manufacturer.DisplayOrder = manufacturerModel.DisplayOrder;
                manufacturer.UpdatedOnUtc = DateTime.UtcNow;

                _commandRepository.UpdateAsync(manufacturer);
                await _commandRepository.SaveChangesAsync();

                result.Data = manufacturerModel;

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
            var manufacturer = await _queryRepository.Table<Manufacturer>()
                .Include(x => x.ProductManufacturers)
                .Include(x => x.Discounts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (manufacturer is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Manufacturer not found";
                return result;
            }

            if (manufacturer.Discounts.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Message = "The Manufacturer Has Any Child";
                return result;
            }

            if (manufacturer.ProductManufacturers.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Message = "The Manufacturer Has Any Child";
                return result;
            }

            _commandRepository.DeleteAsync(manufacturer);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}