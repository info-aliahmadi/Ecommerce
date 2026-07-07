using EFCoreSecondLevelCacheInterceptor;
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

        public async Task<Result<List<ManufacturerDisplayModel>>> GetPublishedManufacturers()
        {
            var result = new Result<List<ManufacturerDisplayModel>>();

            var list = await (from manufacturer in _queryRepository.Table<Manufacturer>()
                              select new ManufacturerDisplayModel()
                              {
                                  Id = manufacturer.Id,
                                  Name = manufacturer.Name,
                                  MetaKeywords = manufacturer.MetaKeywords,
                                  MetaTitle = manufacturer.MetaTitle,
                                  Description = manufacturer.Description,
                                  MetaDescription = manufacturer.MetaDescription,
                                  ImagePreview = new FileStorage.Core.Models.FileUploadModel(manufacturer.ImagePreview),
                                  DisplayOrder = manufacturer.DisplayOrder,
                                  ProductsCount = manufacturer.ProductManufacturers.Where(x => !x.Product.Deleted && x.Product.Published).Count(),
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

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
                                  Published = manufacturer.Published,
                                  Deleted = manufacturer.Deleted,
                                  DisplayOrder = manufacturer.DisplayOrder,
                                  CreatedOnUtc = manufacturer.CreatedOnUtc,
                                  UpdatedOnUtc = manufacturer.UpdatedOnUtc,
                                  ImagePreviewId = manufacturer.ImagePreviewId,
                                  ImagePreview = new FileStorage.Core.Models.FileUploadModel(manufacturer.ImagePreview),
                              }).OrderBy(x => x.DisplayOrder).Cacheable().ToListAsync();
            result.Data = list;
            return result;
        }

        public async Task<Result<List<ManufacturerModel>>> GetListForSelect()
        {
            return await GetManufacturersList();
        }

        public Result<ManufacturerModel> GetById(int id)
        {
            var result = new Result<ManufacturerModel>();
            var list = GetManufacturersList().GetAwaiter().GetResult();
            result.Data = list.Data?.FirstOrDefault(x => x.Id == id) ?? new ManufacturerModel();
            return result;
        }

        public async Task<Result<ManufacturerModel>> Add(ManufacturerModel manufacturerModel)
        {
            var result = new Result<ManufacturerModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Manufacturer>().AnyAsync(x => x.Name == manufacturerModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(manufacturerModel.Id), "The Name already exists"));
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
                    ImagePreviewId = manufacturerModel.ImagePreviewId,
                    Published = manufacturerModel.Published,
                    Deleted = false,
                    DisplayOrder = manufacturerModel.DisplayOrder,
                    CreatedOnUtc = date,
                    UpdatedOnUtc = date
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

                var isExist = await _queryRepository.Table<Manufacturer>().AnyAsync(x => x.Id != manufacturerModel.Id && x.Name == manufacturerModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exists";
                    result.Errors.Add(new Error(nameof(manufacturerModel.Id), "The Name already exists"));
                    return result;
                }

                manufacturer.Name = manufacturerModel.Name;
                manufacturer.MetaKeywords = manufacturerModel.MetaKeywords;
                manufacturer.MetaTitle = manufacturerModel.MetaTitle;
                manufacturer.Description = manufacturerModel.Description;
                manufacturer.MetaDescription = manufacturerModel.MetaDescription;
                manufacturer.ImagePreviewId = manufacturerModel.ImagePreviewId;
                manufacturer.Published = manufacturerModel.Published;
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

        public async Task<Result<List<ManufacturerModel>>> UpdateOrder(List<ManufacturerModel> linkModelList)
        {
            var result = new Result<List<ManufacturerModel>>();

            var ids = linkModelList.Select(x => x.Id).ToArray();
            var i = linkModelList.Count;
            foreach (var item in linkModelList)
            {
                item.DisplayOrder = i--;
            }

            var visibleList = _queryRepository.Table<Manufacturer>().Where(x => ids.Contains(x.Id)).ToList();
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

            if (manufacturer.Discounts.Any() || manufacturer.ProductManufacturers.Any())
            {
                result.Status = ResultStatusEnum.InvalidValidation;
                result.Message = "The Manufacturer has associated items and cannot be deleted";
                return result;
            }

            _commandRepository.DeleteAsync(manufacturer);
            await _commandRepository.SaveChangesAsync();

            return result;
        }
    }
}
