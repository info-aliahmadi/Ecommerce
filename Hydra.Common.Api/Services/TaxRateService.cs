using EFCoreSecondLevelCacheInterceptor;
using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Common.Api.Services
{
    public class TaxRateService : ITaxRateService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public TaxRateService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<TaxRateModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<TaxRateModel>>();

            var list = await (from taxRate in _queryRepository.Table<TaxRate>().Include(x => x.TaxCategory).Include(x => x.Country)
                              select new TaxRateModel()
                              {
                                  Id = taxRate.Id,
                                  TaxCategoryId = taxRate.TaxCategoryId,
                                  TaxCategoryName = taxRate.TaxCategory.Name,
                                  CountryId = taxRate.CountryId,
                                  CountryName = taxRate.Country.Name,
                                  Percentage = taxRate.Percentage,

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<TaxRateModel>>> GetTaxRateListForSelect()
        {
            var result = new Result<List<TaxRateModel>>();
            var list = await (from taxCategory in _queryRepository.Table<TaxRate>().Include(x=>x.TaxCategory)
                              select new TaxRateModel()
                              {
                                  Id = taxCategory.Id,
                                  TaxCategoryName = taxCategory.TaxCategory.Name,
                                  Percentage = taxCategory.Percentage,
                              }).OrderByDescending(x => x.Percentage).Cacheable().ToListAsync();

            result.Data = list;

            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<TaxRateModel>> GetById(int id)
        {
            var result = new Result<TaxRateModel>();
            var taxRate = await _queryRepository.Table<TaxRate>().FirstOrDefaultAsync(x => x.Id == id);

            var taxRateModel = new TaxRateModel()
            {
                Id = taxRate.Id,
                TaxCategoryId = taxRate.TaxCategoryId,
                CountryId = taxRate.CountryId,
                Percentage = taxRate.Percentage,

            };
            result.Data = taxRateModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        public async Task<Result<TaxRateModel>> Add(TaxRateModel taxRateModel)
        {
            var result = new Result<TaxRateModel>();
            try
            {
                bool isExist = await _queryRepository.Table<TaxRate>().AnyAsync(x =>
                                                x.TaxCategoryId == taxRateModel.TaxCategoryId &&
                                                x.CountryId == taxRateModel.CountryId);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(taxRateModel.Id), "The Id already exist"));
                    return result;
                }
                var taxRate = new TaxRate()
                {
                    TaxCategoryId = taxRateModel.TaxCategoryId,
                    CountryId = taxRateModel.CountryId,
                    Percentage = taxRateModel.Percentage,
                };

                await _commandRepository.InsertAsync(taxRate);
                await _commandRepository.SaveChangesAsync();

                taxRateModel.Id = taxRate.Id;

                result.Data = taxRateModel;

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
        /// <param name="taxRateModel"></param>
        /// <returns></returns>
        public async Task<Result<TaxRateModel>> Update(TaxRateModel taxRateModel)
        {
            var result = new Result<TaxRateModel>();
            try
            {
                var taxRate = await _queryRepository.Table<TaxRate>().FirstAsync(x => x.Id == taxRateModel.Id);
                if (taxRate is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The TaxRate not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<TaxRate>().AnyAsync(x =>
                                            x.Id != taxRateModel.Id &&
                                            x.TaxCategoryId == taxRateModel.TaxCategoryId &&
                                            x.CountryId == taxRateModel.CountryId);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The TaxRate already exist";
                    result.Errors.Add(new Error(nameof(taxRateModel.Id), "The TaxRate already exist"));
                    return result;
                }
                taxRate.TaxCategoryId = taxRateModel.TaxCategoryId;
                taxRate.CountryId = taxRateModel.CountryId;
                taxRate.Percentage = taxRateModel.Percentage;

                _commandRepository.Update(taxRate);
                await _commandRepository.SaveChangesAsync();

                result.Data = taxRateModel;

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
            var taxRate = await _queryRepository.Table<TaxRate>().FirstOrDefaultAsync(x => x.Id == id);
            if (taxRate is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The TaxRate not found";
                return result;
            }

            _commandRepository.Delete(taxRate);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}