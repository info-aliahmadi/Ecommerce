using EFCoreSecondLevelCacheInterceptor;
using Hydra.Ecommerce.Core.Domain;
using Hydra.FileStorage.Core.Domain;
using Hydra.FileStorage.Core.Models;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        /// For AdminPage
        /// </summary>
        /// <returns></returns>
        public Result<List<CategoryModel>> GetPublishedCategories()
        {
            var result = new Result<List<CategoryModel>>();

            var list = GetCategories(true);

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        private List<CategoryModel> GetCategories(bool? isPublished = false)
        {
            var query = (from category in _queryRepository.Table<Category>()
                         select new CategoryModel()
                         {
                             Id = category.Id,
                             Name = category.Name,
                             Key = category.Key,
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
                             Color = category.Color,
                             ProductsCount = category.ProductCategories.Where(c => c.Product.Published == isPublished && c.Product.Deleted == !isPublished).Count()
                         }).Where(x => x.Deleted == false);

            if (isPublished != null)
            {
                query = query.Where(x => x.Published == isPublished && x.Deleted == !isPublished);
            }
            var list = query.OrderBy(x => x.DisplayOrder).Cacheable().ToList();

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

            result.Data = GetCategories();

            return result;
        }


        /// <summary>
        /// For AdminPage
        /// </summary>
        /// <returns></returns>
        public Result<List<CategoryModel>> GetHierarchy()
        {
            var result = new Result<List<CategoryModel>>();

            var list = GetCategories();

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

        /// <summary>
        /// Recursively retrieves the immediate child categories of the specified category, including all nested
        /// descendants.
        /// </summary>
        /// <remarks>The returned list includes all levels of descendants, with each child's Childs
        /// collection populated recursively. The method does not modify the input list except for updating the Childs
        /// property of the returned category objects.</remarks>
        /// <param name="topic">The parent category for which to retrieve child categories. Cannot be null.</param>
        /// <param name="topics">The complete list of available categories to search for child relationships. Cannot be null.</param>
        /// <returns>A list of child categories directly under the specified parent category, including their nested descendants.
        /// Returns null if the parent category has no children.</returns>
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
        /// Retrieves a list of categories formatted for use in selection controls, excluding deleted categories.
        /// </summary>
        /// <remarks>The returned list includes only categories that are not marked as deleted.
        /// Parent-child relationships are preserved to support hierarchical selection scenarios.</remarks>
        /// <returns>A <see cref="Result{T}"/> containing a list of <see cref="CategoryModel"/> objects representing the
        /// available categories for selection. The list will be empty if no categories are available.</returns>
        public Result<List<CategoryModel>> GetListForSelect()
        {
            var result = new Result<List<CategoryModel>>();

            var list = GetCategories().Where(x => !x.Deleted).Select(category => new CategoryModel()
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
        /// <summary>
        /// Recursively finds and returns the immediate child categories of the specified category within the provided
        /// list.
        /// </summary>
        /// <remarks>This method modifies the Name property of the input category by prefixing it with the
        /// specified indentation string. It also adds the processed category to an external result list. The method is
        /// intended for use in recursive traversal scenarios, such as building hierarchical category
        /// structures.</remarks>
        /// <param name="topic">The parent category for which to find child categories. The category's name will be prefixed with the
        /// specified indentation.</param>
        /// <param name="space">A string used to prefix the category name, typically for indentation purposes in hierarchical displays.</param>
        /// <param name="topics">The complete list of categories to search for child categories. Must not be null.</param>
        /// <returns>A list of immediate child categories of the specified parent category. Returns null if no child categories
        /// are found.</returns>
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

            result.Data = GetCategories().FirstOrDefault(x => x.Id == id) ?? new CategoryModel();

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
                    Key = categoryModel.Key,
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
                    Color = categoryModel.Color,
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
                category.Key = categoryModel.Key;
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
                category.Color = categoryModel.Color;
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