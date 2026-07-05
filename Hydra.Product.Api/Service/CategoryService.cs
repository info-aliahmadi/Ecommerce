using EFCoreSecondLevelCacheInterceptor;
using Hydra.Ecommerce.Core.Domain;
using Hydra.Kernel.GeneralModels;
using Hydra.Kernel.Interface;
using Hydra.Product.Core.Interfaces;
using Hydra.Product.Core.Models;
using Microsoft.EntityFrameworkCore;

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

        public Result<List<CategoryDisplayModel>> GetPublishedHerarchyCategories()
        {
            var displayList = GetPublishedCategories();
            var list = displayList.Select(d => new CategoryDisplayModel
            {
                Id = d.Id,
                Name = d.Name,
                Key = d.Key,
                MetaKeywords = d.MetaKeywords,
                MetaTitle = d.MetaTitle,
                Description = d.Description,
                MetaDescription = d.MetaDescription,
                ParentCategoryId = d.ParentCategoryId,
                ShowOnHomepage = d.ShowOnHomepage,
                DisplayOrder = d.DisplayOrder,
                ImagePreviewPath = d.ImagePreviewPath,
                Color = d.Color
            }).ToList();

            var parents = list.Where(x => x.ParentCategoryId == null).ToList();

            foreach (var parent in parents)
            {
                var children = GetChildren(parent, list);
                parent.Childs.AddRange(children);
            }

            return new Result<List<CategoryDisplayModel>> { Data = parents };
        }

        private List<CategoryDisplayModel> GetPublishedCategories()
        {
            return _queryRepository.Table<Category>()
                .Where(x => !x.Deleted && x.Published)
                .Select(category => new CategoryDisplayModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Key = category.Key,
                    MetaKeywords = category.MetaKeywords,
                    MetaTitle = category.MetaTitle,
                    Description = category.Description,
                    MetaDescription = category.MetaDescription,
                    ParentCategoryId = category.ParentCategoryId,
                    ImagePreviewPath = category.ImagePreview.Directory + category.ImagePreview.FileName,
                    ShowOnHomepage = category.ShowOnHomepage,
                    DisplayOrder = category.DisplayOrder,
                    Color = category.Color,
                    ProductsCount = category.ProductCategories.Count(c => c.Product.Published && !c.Product.Deleted)
                })
                .OrderBy(x => x.DisplayOrder)
                .Cacheable()
                .ToList();
        }

        public Result<List<CategoryModel>> GetList()
        {
            var categories = GetAllCategories();
            return new Result<List<CategoryModel>> { Data = categories };
        }

        public Result<List<CategoryModel>> GetHierarchy()
        {
            var list = GetAllCategories();
            var parents = list.Where(x => x.ParentCategoryId == null).ToList();

            foreach (var parent in parents)
            {
                var children = GetChildren(parent, list);
                parent.Childs.AddRange(children);
            }

            return new Result<List<CategoryModel>> { Data = parents };
        }

        public Result<List<CategoryModel>> GetListForSelect()
        {
            var categories = GetAllCategories();
            var result = new List<CategoryModel>();
            var parents = categories.Where(x => x.ParentCategoryId == null).ToList();

            foreach (var parent in parents)
            {
                FlattenForSelect(parent, "", categories, result);
            }

            return new Result<List<CategoryModel>> { Data = result };
        }

        public Result<CategoryModel> GetById(int id)
        {
            var category = GetAllCategories().FirstOrDefault(x => x.Id == id);
            return new Result<CategoryModel> { Data = category ?? new CategoryModel() };
        }

        public async Task<Result<CategoryModel>> Add(CategoryModel categoryModel)
        {
            var result = new Result<CategoryModel>();

            if (await _queryRepository.Table<Category>().AnyAsync(x => x.Id == categoryModel.Id))
            {
                result.Status = ResultStatusEnum.ItsDuplicate;
                result.Errors.Add(new Error(nameof(categoryModel.Id), "The Id already exist"));
                return result;
            }

            var category = MapToEntity(categoryModel);
            await _commandRepository.InsertAsync(category);
            await _commandRepository.SaveChangesAsync();

            categoryModel.Id = category.Id;
            result.Data = categoryModel;
            return result;
        }

        public async Task<Result<CategoryModel>> Update(CategoryModel categoryModel)
        {
            var result = new Result<CategoryModel>();

            var category = await _queryRepository.Table<Category>().FirstOrDefaultAsync(x => x.Id == categoryModel.Id);
            if (category is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Category not found";
                return result;
            }

            if (await _queryRepository.Table<Category>().AnyAsync(x => x.Id != categoryModel.Id && x.Name == categoryModel.Name))
            {
                result.Status = ResultStatusEnum.ItsDuplicate;
                result.Errors.Add(new Error(nameof(categoryModel.Name), "The Name already exist"));
                return result;
            }

            MapToEntity(categoryModel, category);
            _commandRepository.UpdateAsync(category);
            await _commandRepository.SaveChangesAsync();

            result.Data = categoryModel;
            return result;
        }

        public async Task<Result<List<CategoryModel>>> UpdateOrder(List<CategoryModel> categoriesList)
        {
            var flattenMenus = FlattenEditedItems(categoriesList);
            var editedIds = flattenMenus.Select(x => x.Id).ToArray();

            var editedCategories = await _queryRepository.Table<Category>()
                .Where(x => editedIds.Contains(x.Id))
                .ToListAsync();

            foreach (var category in editedCategories)
            {
                var model = flattenMenus.First(x => x.Id == category.Id);
                category.DisplayOrder = model.DisplayOrder;
                category.ParentCategoryId = model.ParentCategoryId;
                _commandRepository.UpdateAsync(category);
            }

            await _commandRepository.SaveChangesAsync();

            return new Result<List<CategoryModel>> { Data = categoriesList };
        }

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
        /// <summary>
        /// Retrieves all categories that are not marked as deleted, ordered by display order.
        /// </summary>
        /// <remarks>The returned list excludes categories where the Deleted flag is set to <see
        /// langword="true"/>. Results are ordered by the DisplayOrder property. The query may use caching to improve
        /// performance.</remarks>
        /// <returns>A list of <see cref="CategoryModel"/> objects representing all active categories. The list is empty if no
        /// categories are available.</returns>
        private List<CategoryModel> GetAllCategories()
        {
            return _queryRepository.Table<Category>()
                .Where(x => !x.Deleted)
                .Select(category => new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Key = category.Key,
                    MetaKeywords = category.MetaKeywords,
                    MetaTitle = category.MetaTitle,
                    Description = category.Description,
                    MetaDescription = category.MetaDescription,
                    ParentCategoryId = category.ParentCategoryId,
                    ShowOnHomepage = category.ShowOnHomepage,
                    Published = category.Published,
                    Deleted = category.Deleted,
                    DisplayOrder = category.DisplayOrder,
                    CreatedOnUtc = category.CreatedOnUtc,
                    UpdatedOnUtc = category.UpdatedOnUtc,
                    ImagePreviewId = category.ImagePreviewId,
                    ImagePreview = category.ImagePreview != null?  new FileStorage.Core.Models.FileUploadModel
                    {
                        Id = category.ImagePreview.Id,
                        Directory = category.ImagePreview.Directory,
                        FileName = category.ImagePreview.FileName
                    } : null,
                    Color = category.Color
                })
                .OrderBy(x => x.DisplayOrder)
                .Cacheable()
                .ToList();
        }
        /// <summary>
        /// Retrieves the immediate and nested child categories of the specified parent category from the provided list.
        /// </summary>
        /// <remarks>The returned list includes both direct and indirect (nested) child categories. The
        /// method recursively populates the Childs property of each child category with its own children.</remarks>
        /// <param name="parent">The parent category for which to find all child categories. Cannot be null.</param>
        /// <param name="allCategories">The complete list of categories to search for child categories. Cannot be null.</param>
        /// <returns>A list of child categories belonging to the specified parent, including all descendants in the hierarchy.
        /// Returns an empty list if the parent has no children.</returns>
        private static List<CategoryModel> GetChildren(CategoryModel parent, List<CategoryModel> allCategories)
        {
            var children = allCategories.Where(x => x.ParentCategoryId == parent.Id).ToList();

            foreach (var child in children)
            {
                child.Childs.AddRange(GetChildren(child, allCategories));
            }

            return children;
        }

        /// <summary>
        /// Retrieves the immediate and nested child categories of the specified parent category from the provided list.
        /// </summary>
        /// <remarks>The returned list includes both direct and indirect (nested) child categories. The
        /// method recursively populates the Childs property of each child category with its own children.</remarks>
        /// <param name="parent">The parent category for which to find all child categories. Cannot be null.</param>
        /// <param name="allCategories">The complete list of categories to search for child categories. Cannot be null.</param>
        /// <returns>A list of child categories belonging to the specified parent, including all descendants in the hierarchy.
        /// Returns an empty list if the parent has no children.</returns>
        private static List<CategoryDisplayModel> GetChildren(CategoryDisplayModel parent, List<CategoryDisplayModel> allCategories)
        {
            var children = allCategories.Where(x => x.ParentCategoryId == parent.Id).ToList();

            foreach (var child in children)
            {
                child.Childs.AddRange(GetChildren(child, allCategories));
            }

            return children;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="indent"></param>
        /// <param name="allCategories"></param>
        /// <param name="result"></param>
        private static void FlattenForSelect(CategoryModel category, string indent, List<CategoryModel> allCategories, List<CategoryModel> result)
        {
            result.Add(new CategoryModel { Id = category.Id, Name = indent + category.Name });

            foreach (var child in allCategories.Where(x => x.ParentCategoryId == category.Id))
            {
                FlattenForSelect(child, indent + "    ", allCategories, result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        private static List<CategoryModel> FlattenEditedItems(List<CategoryModel> categories)
        {
            var result = new List<CategoryModel>();

            foreach (var item in categories)
            {
                if (item.IsEdited)
                    result.Add(item);

                if (item.Childs.Any())
                    result.AddRange(FlattenEditedItems(item.Childs));
            }

            return result;
        }

        private static Category MapToEntity(CategoryModel model)
        {
            return new Category
            {
                Name = model.Name,
                Key = model.Key,
                MetaKeywords = model.MetaKeywords,
                MetaTitle = model.MetaTitle,
                Description = model.Description,
                MetaDescription = model.MetaDescription,
                ParentCategoryId = model.ParentCategoryId,
                ImagePreviewId = model.ImagePreviewId,
                ShowOnHomepage = model.ShowOnHomepage,
                Published = model.Published,
                Deleted = model.Deleted,
                DisplayOrder = model.DisplayOrder,
                CreatedOnUtc = model.CreatedOnUtc,
                UpdatedOnUtc = model.UpdatedOnUtc,
                Color = model.Color
            };
        }

        private static void MapToEntity(CategoryModel model, Category entity)
        {
            entity.Name = model.Name;
            entity.Key = model.Key;
            entity.MetaKeywords = model.MetaKeywords;
            entity.MetaTitle = model.MetaTitle;
            entity.Description = model.Description;
            entity.MetaDescription = model.MetaDescription;
            entity.ParentCategoryId = model.ParentCategoryId;
            entity.ImagePreviewId = model.ImagePreviewId;
            entity.ShowOnHomepage = model.ShowOnHomepage;
            entity.Published = model.Published;
            entity.Deleted = model.Deleted;
            entity.DisplayOrder = model.DisplayOrder;
            entity.CreatedOnUtc = model.CreatedOnUtc;
            entity.UpdatedOnUtc = model.UpdatedOnUtc;
            entity.Color = model.Color;
        }
    }
}
