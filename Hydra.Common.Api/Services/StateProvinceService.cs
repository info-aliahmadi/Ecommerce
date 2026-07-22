using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.Kernel.Extension;

namespace Hydra.Common.Api.Services
{
    public class StateProvinceService : IStateProvinceService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public StateProvinceService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<List<StateProvinceModel>>> GetListForSelect(int countryId)
        {
            var result = new Result<List<StateProvinceModel>>();

            var list = await _queryRepository.Table<StateProvince>().Include(x => x.Country)
                .Where(stateProvince => stateProvince.CountryId == countryId && stateProvince.Published == true)
                .Select(stateProvince => new StateProvinceModel()
                {
                    Id = stateProvince.Id,
                    Name = stateProvince.Name,
                    Abbreviation = stateProvince.Abbreviation,
                    CountryId = stateProvince.CountryId,
                    CountryName = stateProvince.Country.Name,
                    Published = stateProvince.Published,
                    DisplayOrder = stateProvince.DisplayOrder

                }).OrderByDescending(x => x.Id).ToListAsync();

            result.Data = list;

            return result;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<StateProvinceModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<StateProvinceModel>>();

            var list = await (from stateProvince in _queryRepository.Table<StateProvince>().Include(x => x.Country)
                              select new StateProvinceModel()
                              {
                                  Id = stateProvince.Id,
                                  Name = stateProvince.Name,
                                  Abbreviation = stateProvince.Abbreviation,
                                  CountryId = stateProvince.CountryId,
                                  CountryName = stateProvince.Country.Name,
                                  Published = stateProvince.Published,
                                  DisplayOrder = stateProvince.DisplayOrder

                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<StateProvinceModel>> GetById(int id)
        {
            var result = new Result<StateProvinceModel>();
            var stateProvince = await _queryRepository.Table<StateProvince>().FirstOrDefaultAsync(x => x.Id == id);

            var stateProvinceModel = new StateProvinceModel()
            {
                Id = stateProvince.Id,
                Name = stateProvince.Name,
                Abbreviation = stateProvince.Abbreviation,
                CountryId = stateProvince.CountryId,
                Published = stateProvince.Published,
                DisplayOrder = stateProvince.DisplayOrder,
                //Addresses = stateProvince.Addresses,
                //TaxRates = stateProvince.TaxRates,

            };
            result.Data = stateProvinceModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stateProvinceModel"></param>
        /// <returns></returns>
        public async Task<Result<StateProvinceModel>> Add(StateProvinceModel stateProvinceModel)
        {
            var result = new Result<StateProvinceModel>();
            try
            {
                bool isExist = await _queryRepository.Table<StateProvince>().AnyAsync(x =>
                                                                                        x.CountryId == stateProvinceModel.CountryId
                                                                                        && x.Name == stateProvinceModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(stateProvinceModel.Id), "The Id already exist"));
                    return result;
                }
                var stateProvince = new StateProvince()
                {
                    Name = stateProvinceModel.Name,
                    Abbreviation = stateProvinceModel.Abbreviation,
                    CountryId = stateProvinceModel.CountryId,
                    Published = stateProvinceModel.Published,
                    DisplayOrder = stateProvinceModel.DisplayOrder,
                    //Addresses = stateProvinceModel.Addresses,
                    //TaxRates = stateProvinceModel.TaxRates,

                };

                await _commandRepository.InsertAsync(stateProvince);
                await _commandRepository.SaveChangesAsync();

                stateProvinceModel.Id = stateProvince.Id;

                result.Data = stateProvinceModel;

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
        /// <param name="stateProvinceModel"></param>
        /// <returns></returns>
        public async Task<Result<StateProvinceModel>> Update(StateProvinceModel stateProvinceModel)
        {
            var result = new Result<StateProvinceModel>();
            try
            {
                var stateProvince = await _queryRepository.Table<StateProvince>().FirstAsync(x => x.Id == stateProvinceModel.Id);
                if (stateProvince is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The StateProvince not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<StateProvince>().AnyAsync(x => x.Id != stateProvinceModel.Id &&
                                                                                            x.CountryId == stateProvinceModel.CountryId &&
                                                                                            x.Name == stateProvinceModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(stateProvinceModel.Id), "The Id already exist"));
                    return result;
                }
                stateProvince.Name = stateProvinceModel.Name;
                stateProvince.Abbreviation = stateProvinceModel.Abbreviation;
                stateProvince.CountryId = stateProvinceModel.CountryId;
                stateProvince.Published = stateProvinceModel.Published;
                stateProvince.DisplayOrder = stateProvinceModel.DisplayOrder;
                //stateProvince.Addresses = stateProvinceModel.Addresses;
                //stateProvince.TaxRates = stateProvinceModel.TaxRates;

                _commandRepository.Update(stateProvince);
                await _commandRepository.SaveChangesAsync();

                result.Data = stateProvinceModel;

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
            var stateProvince = await _queryRepository.Table<StateProvince>().FirstOrDefaultAsync(x => x.Id == id);
            if (stateProvince is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The StateProvince not found";
                return result;
            }

            _commandRepository.Delete(stateProvince);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}