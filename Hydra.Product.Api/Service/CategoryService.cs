using EFCoreSecondLevelCacheInterceptor;
using Hydra.Kernel.Interface;
using Hydra.Kernel.GeneralModels;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using Hydra.FileStorage.Core.Domain;
using Hydra.FileStorage.Core.Models;

namespace Hydra.Product.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        public CategoryService(IQueryRepository queryRepository, ICommandRepository commandRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private List<CategoryModel> GetCategoriesList()
        {

            var list = (from category in _queryRepository.Table<Category>()
                        select new CategoryModel()
                        {
                            Id = category.Id,
                            Name = category.Name,
                            MetaKeywords = category.MetaKeywords,
                            MetaTitle = category.MetaTitle,
                            Description = category.Description,
                            MetaDescription = category.MetaDescription,
                            ParentCategoryId = category.ParentCategoryId,
                            PictureId = category.PictureId,
                            ShowOnHomepage = category.ShowOnHomepage,
                            Published = category.Published,
                            Deleted = category.Deleted,
                            DisplayOrder = category.DisplayOrder,
                            CreatedOnUtc = category.CreatedOnUtc,
                            UpdatedOnUtc = category.UpdatedOnUtc,


                        }).Where(x => x.Deleted == false).OrderBy(x => x.DisplayOrder).Cacheable().ToList();

            var listIds = list.Where(x => x.PictureId != null).Select(x => x.PictureId).ToArray();
            var files = _queryRepository.Table<FileUpload>().Where(x => listIds.Contains(x.Id));
            foreach (var item in list)
            {
                var file = files.FirstOrDefault(x => x.Id == item.PictureId);
                if (file != null)
                    item.PictureInfo = new FileUploadModel()
                    {
                        Id = file.Id,
                        FileName = file.FileName,
                        Directory = file.Directory,
                        Extension = file.Extension,
                        Size = file.Size,
                        Thumbnail = file.Thumbnail
                    };
            }

            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public Result<List<CategoryModel>> GetList()
        {
            var result = new Result<List<CategoryModel>>();

            result.Data = GetCategoriesList();

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Result<List<CategoryModel>> GetHierarchy()
        {
            var result = new Result<List<CategoryModel>>();

            var list = GetCategoriesList();

            var parents = list.Where(x => x.ParentCategoryId == null).ToList();
            foreach (var topic in parents)
            {
                var childs = GetChild(topic, list);
                if (childs != null)
                {
                    topic.Childs.AddRange(childs);
                }
            }

            result.Data = parents;

            return result;
        }
        private List<CategoryModel> GetChild(CategoryModel topic, List<CategoryModel> topics)
        {

            var result = topics.Where(x => x.ParentCategoryId == topic.Id).ToList();
            if (result.Count == 0) return null;
            foreach (var item in result)
            {
                var childs = GetChild(item, topics);
                if (childs != null)
                    item.Childs.AddRange(childs);
            }
            return result;
        }


        List<CategoryModel> resultList = new List<CategoryModel>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Result<List<CategoryModel>> GetListForSelect()
        {
            var result = new Result<List<CategoryModel>>();

            var list = GetCategoriesList().Where(x => !x.Deleted).Select(category => new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name
            }).ToList();

            var parents = list.Where(x => x.ParentCategoryId == null).ToList();

            foreach (var category in parents)
            {
                GetChildOfSelect(category, "", list);
            }

            result.Data = resultList;

            return result;
        }

        private List<CategoryModel> GetChildOfSelect(CategoryModel topic, string space, List<CategoryModel> topics)
        {
            topic.Name = space + topic.Name;
            resultList.Add(topic);

            var childs = topics.Where(x => x.ParentCategoryId == topic.Id).ToList();
            if (childs.Count == 0)
            {
                return null;
            }
            foreach (var item in childs)
            {
                GetChildOfSelect(item, space + "    ", topics);
            }
            return childs;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<CategoryModel> GetById(int id)
        {
            var result = new Result<CategoryModel>();

            result.Data = GetCategoriesList().FirstOrDefault(x => x.Id == id) ?? new CategoryModel();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        public async Task<Result<CategoryModel>> Add(CategoryModel categoryModel)
        {
            var result = new Result<CategoryModel>();
            try
            {
                bool isExist = await _queryRepository.Table<Category>().AnyAsync(x => x.Id == categoryModel.Id);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Id already exist";
                    result.Errors.Add(new Error(nameof(categoryModel.Id), "The Id already exist"));
                    return result;
                }
                var category = new Category()
                {
                    Name = categoryModel.Name,
                    MetaKeywords = categoryModel.MetaKeywords,
                    MetaTitle = categoryModel.MetaTitle,
                    Description = categoryModel.Description,
                    MetaDescription = categoryModel.MetaDescription,
                    ParentCategoryId = categoryModel.ParentCategoryId,
                    PictureId = categoryModel.PictureId,
                    ShowOnHomepage = categoryModel.ShowOnHomepage,
                    Published = categoryModel.Published,
                    Deleted = categoryModel.Deleted,
                    DisplayOrder = categoryModel.DisplayOrder,
                    CreatedOnUtc = categoryModel.CreatedOnUtc,
                    UpdatedOnUtc = categoryModel.UpdatedOnUtc,
                    //ProductCategories = categoryModel.ProductCategories,
                    //Discounts = categoryModel.Discounts,

                };

                await _commandRepository.InsertAsync(category);
                await _commandRepository.SaveChangesAsync();

                categoryModel.Id = category.Id;

                result.Data = categoryModel;

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
        /// <param name="categoryModel"></param>
        /// <returns></returns>
        public async Task<Result<CategoryModel>> Update(CategoryModel categoryModel)
        {
            var result = new Result<CategoryModel>();
            try
            {
                var category = await _queryRepository.Table<Category>().FirstAsync(x => x.Id == categoryModel.Id);
                if (category is null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Category not found";
                    return result;
                }
                bool isExist = await _queryRepository.Table<Category>().AnyAsync(x => x.Id != categoryModel.Id && x.Name == categoryModel.Name);
                if (isExist)
                {
                    result.Status = ResultStatusEnum.ItsDuplicate;
                    result.Message = "The Name already exist";
                    result.Errors.Add(new Error(nameof(categoryModel.Id), "The Name already exist"));
                    return result;
                }
                category.Name = categoryModel.Name;
                category.MetaKeywords = categoryModel.MetaKeywords;
                category.MetaTitle = categoryModel.MetaTitle;
                category.Description = categoryModel.Description;
                category.MetaDescription = categoryModel.MetaDescription;
                category.ParentCategoryId = categoryModel.ParentCategoryId;
                category.PictureId = categoryModel.PictureId;
                category.ShowOnHomepage = categoryModel.ShowOnHomepage;
                category.Published = categoryModel.Published;
                category.Deleted = categoryModel.Deleted;
                category.DisplayOrder = categoryModel.DisplayOrder;
                category.CreatedOnUtc = categoryModel.CreatedOnUtc;
                category.UpdatedOnUtc = categoryModel.UpdatedOnUtc;
                //category.ProductCategories = categoryModel.ProductCategories;
                //category.Discounts = categoryModel.Discounts;

                _commandRepository.UpdateAsync(category);
                await _commandRepository.SaveChangesAsync();

                result.Data = categoryModel;

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
        /// <param name="linkModelList"></param>
        /// <returns></returns>
        public async Task<Result<List<CategoryModel>>> UpdateOrder(List<CategoryModel> categoriesList)
        {
            var result = new Result<List<CategoryModel>>();

            var flattenMenus = new List<CategoryModel>();

            GetChild(categoriesList);

            void GetChild(List<CategoryModel> menus)
            {
                foreach (var item in menus)
                {
                    if (item.IsEdited)
                    {
                        flattenMenus.Add(item);
                    }
                    if (item.Childs.Any())
                    {
                        GetChild(item.Childs);
                    }
                }
            }

            var editedModelIds = flattenMenus.Select(x => x.Id).ToArray();


            var editedList = _queryRepository.Table<Category>().Where(x => editedModelIds.Contains(x.Id)).ToList();

            foreach (var item in editedList)
            {
                var model = flattenMenus.First(x => x.Id == item.Id);
                item.DisplayOrder = model.DisplayOrder;
                item.ParentCategoryId = model.ParentCategoryId;
                _commandRepository.UpdateAsync(item);
            }

            await _commandRepository.SaveChangesAsync();

            result.Data = categoriesList;
            return result;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            var category = await _queryRepository.Table<Category>().FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Category not found";
                return result;
            }

            category.Deleted = true;

            _commandRepository.UpdateAsync(category);

            await _commandRepository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Remove(int id)
        {
            var result = new Result();
            var category = await _queryRepository.Table<Category>().FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Category not found";
                return result;
            }

            _commandRepository.DeleteAsync(category);
            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}