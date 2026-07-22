using Hydra.Common.Core.Interfaces;
using Hydra.Common.Core.Models;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Kernel.Extension;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Microsoft.EntityFrameworkCore;

namespace Hydra.Common.Api.Services
{
    public class AddressService : IAddressService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public AddressService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<List<AddressModel>>> GetUserAddresses(int userId)
        {
            var result = new Result<List<AddressModel>>();
            var userAddressModel = await _queryRepository.Table<Address>()
                .Include(x => x.Country)
                .ThenInclude(x => x.StateProvinces)
                .Where(x => x.UserId == userId).Select(address => new AddressModel()
                {
                    UserId = address.UserId,
                    CountryId = address.CountryId,
                    CountryName = address.Country.Name,
                    StateProvinceId = address.StateProvinceId,
                    StateProvinceName = address.StateProvince.Name,
                    City = address.City,
                    County = address.County,
                    Title = address.Title,
                    PhoneNumber = address.PhoneNumber,
                    Address1 = address.Address1,
                    ZipPostalCode = address.ZipPostalCode,
                    GeoLocation = address.GeoLocation,
                    CreatedOnUtc = address.CreatedOnUtc,
                    IsDefault = address.IsDefault,
                }).ToListAsync();

            result.Data = userAddressModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<PaginatedList<AddressModel>>> GetList(GridDataBound dataGrid)
        {
            var result = new Result<PaginatedList<AddressModel>>();

            var list = await (from address in _queryRepository.Table<Address>()
                .Include(x => x.Country)
                .ThenInclude(x => x.StateProvinces)
                              select new AddressModel()
                              {
                                  Id = address.Id,
                                  UserId = address.UserId,
                                  CountryId = address.CountryId,
                                  CountryName = address.Country.Name,
                                  StateProvinceId = address.StateProvinceId,
                                  StateProvinceName = address.StateProvince.Name,
                                  City = address.City,
                                  County = address.County,
                                  Title = address.Title,
                                  PhoneNumber = address.PhoneNumber,
                                  Address1 = address.Address1,
                                  ZipPostalCode = address.ZipPostalCode,
                                  GeoLocation = address.GeoLocation,
                                  CreatedOnUtc = address.CreatedOnUtc,
                                  IsDefault = address.IsDefault,
                              }).OrderByDescending(x => x.Id).ToPaginatedListAsync(dataGrid);

            result.Data = list;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<AddressModel>> GetById(int id)
        {
            var result = new Result<AddressModel>();
            var address = await _queryRepository.Table<Address>()
                .Include(x => x.Country)
                .ThenInclude(x => x.StateProvinces)
                .FirstOrDefaultAsync(x => x.Id == id);

            var addressModel = new AddressModel()
            {
                Id = address.Id,
                UserId = address.UserId,
                CountryId = address.CountryId,
                CountryName = address.Country.Name,
                StateProvinceId = address.StateProvinceId,
                StateProvinceName = address.StateProvince.Name,
                City = address.City,
                County = address.County,
                Title = address.Title,
                PhoneNumber = address.PhoneNumber,
                Address1 = address.Address1,
                ZipPostalCode = address.ZipPostalCode,
                GeoLocation = address.GeoLocation,
                CreatedOnUtc = address.CreatedOnUtc,
                IsDefault = address.IsDefault,

            };
            result.Data = addressModel;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public async Task<Result<AddressModel>> Add(AddressModel addressModel)
        {
            var result = new Result<AddressModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Address>().AnyAsync(x => x.Id == addressModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(addressModel.Id), "The Id already exist"));
                    return result;
                }

                var address = new Address()
                {
                    UserId = addressModel.UserId,
                    CountryId = addressModel.CountryId,
                    StateProvinceId = addressModel.StateProvinceId,
                    City = addressModel.City,
                    County = addressModel.County,
                    Title = addressModel.Title,
                    PhoneNumber = addressModel.PhoneNumber,
                    Address1 = addressModel.Address1,
                    ZipPostalCode = addressModel.ZipPostalCode,
                    GeoLocation = addressModel.GeoLocation,
                    CreatedOnUtc = addressModel.CreatedOnUtc,
                    IsDefault = false,
                };

                await _commandRepository.InsertAsync(address);
                await _commandRepository.SaveChangesAsync();

                await SetAsDefault(addressModel.UserId, address.Id);

                addressModel.Id = address.Id;

                result.Data = addressModel;

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
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public async Task<Result<AddressModel>> Update(AddressModel addressModel)
        {
            var result = new Result<AddressModel>();
            try
            {
                var address = await _queryRepository.Table<Address>().FirstAsync(x => x.Id == addressModel.Id);
                if (address is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Address not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<Address>().AnyAsync(x => x.Id != addressModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(addressModel.Id), "The Id already exist"));
                    return result;
                }
                address.UserId = addressModel.UserId;
                address.CountryId = addressModel.CountryId;
                address.StateProvinceId = addressModel.StateProvinceId;
                address.City = addressModel.City;
                address.County = addressModel.County;
                address.Title = addressModel.Title;
                address.PhoneNumber = addressModel.PhoneNumber;
                address.Address1 = addressModel.Address1;
                address.ZipPostalCode = addressModel.ZipPostalCode;
                address.CreatedOnUtc = addressModel.CreatedOnUtc;
                address.GeoLocation = addressModel.GeoLocation;


                _commandRepository.Update(address);
                await _commandRepository.SaveChangesAsync();

                if (addressModel.IsDefault == true)
                    await SetAsDefault(addressModel.UserId, address.Id);

                result.Data = addressModel;

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
        /// <param name="addressModel"></param>
        /// <returns></returns>
        public async Task<Result<bool>> SetAsDefault(int userId, int addressId)
        {
            var result = new Result<bool>();
            try
            {
                var address = await _queryRepository.Table<Address>().FirstAsync(x => x.Id == addressId);
                if (address is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Address not found";
                    return result;
                }

                var addresses = _queryRepository.Table<Address>().Where(x => x.UserId == userId && x.IsDefault).ToList();
                foreach (var item in addresses)
                {
                    item.IsDefault = false;
                    _commandRepository.Update(item);
                }
                await _commandRepository.SaveChangesAsync();

                address.IsDefault = true;

                _commandRepository.Update(address);
                await _commandRepository.SaveChangesAsync();

                result.Data = true;

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
            var address = await _queryRepository.Table<Address>().FirstOrDefaultAsync(x => x.Id == id);
            if (address is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Address not found";
                return result;
            }

            _commandRepository.Delete(address);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}