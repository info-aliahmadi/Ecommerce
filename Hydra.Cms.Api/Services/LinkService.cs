using EFCoreSecondLevelCacheInterceptor;
using Hydra.Cms.Core.Domain;
using Hydra.Cms.Core.Interfaces;
using Hydra.Cms.Core.Models;
using Hydra.FileStorage.Core.Domain;
using Hydra.FileStorage.Core.Models;
using Hydra.Kernel.Interface;
using Hydra.Kernel.GeneralModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace Hydra.Cms.Api.Services
{
    public class LinkService : ILinkService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly ICommandRepository _commandRepository;
        private readonly ITagService _tagService;

        public LinkService(IQueryRepository queryRepository, ICommandRepository commandRepository, ITagService tagService)
        {
            _queryRepository = queryRepository;
            _commandRepository = commandRepository;
            _tagService = tagService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<List<LinkModel>>> GetList()
        {
            var result = new Result<List<LinkModel>>();

            var linkList = await _queryRepository.Table<Link>().Include(x => x.User).Select(link => new LinkModel()
            {
                Id = link.Id,
                Description = link.Description,
                Url = link.Url,
                Title = link.Title,
                ImagePreviewId = link.ImagePreviewId,
                ImagePreview = new FileUploadModel(link.ImagePreview),
                LinkSectionId = link.LinkSectionId,
                Order = link.Order,
                UserId = link.UserId,
                UserName = link.User.UserName ?? string.Empty,
            }).OrderByDescending(x => x.Order).Cacheable().ToListAsync();

            result.Data = linkList;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public async Task<Result<List<LinkModel>>> GetByKeyList(string sectionKey)
        {
            var result = new Result<List<LinkModel>>();

            var linkList = await _queryRepository.Table<Link>().Include(x => x.LinkSection).Where(x=>x.LinkSection.Key == sectionKey).Select(link => new LinkModel()
            {
                Id = link.Id,
                Description = link.Description,
                Url = link.Url,
                Title = link.Title,
                ImagePreviewId = link.ImagePreviewId,
                LinkSectionId = link.LinkSectionId,
                Order = link.Order,
                UserId = link.UserId
            }).OrderByDescending(x => x.Order).Cacheable().ToListAsync();


            result.Data = linkList;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<LinkModel>> GetById(int id)
        {
            var result = new Result<LinkModel>();
            var link = await _queryRepository.Table<Link>().Include(x => x.User).Include(x => x.LinkSection).Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var linkModel = new LinkModel()
            {
                Id = link.Id,
                Title = link.Title,
                Url = link.Url,
                Description = link.Description,
                LinkSectionId = link.LinkSectionId,
                ImagePreviewId = link.ImagePreviewId,
                ImagePreview = new FileUploadModel(link.ImagePreview),
                LinkSectionKey = link.LinkSection.Key,
                Order = link.Order,
                UserId = link.UserId,
                UserName = link.User.UserName ?? ""
            };

            result.Data = linkModel;

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public async Task<Result<LinkModel>> Add(LinkModel linkModel)
        {
            var result = new Result<LinkModel>();
            try
            {
                var maxOrder = _queryRepository.Table<Link>().OrderByDescending(x => x.Order).FirstOrDefault()?.Order ?? 0;

                var link = new Link()
                {
                    Title = linkModel.Title,
                    Url = linkModel.Url,
                    Description = linkModel.Description,
                    ImagePreviewId = linkModel.ImagePreviewId,
                    LinkSectionId = linkModel.LinkSectionId,
                    Order = maxOrder,
                    UserId = linkModel.UserId
                };

                await _commandRepository.InsertAsync(link);

                await _commandRepository.SaveChangesAsync();

                linkModel.Id = link.Id;

                result.Data = linkModel;

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
        /// <param name="linkModel"></param>
        /// <returns></returns>
        public async Task<Result<LinkModel>> Update(LinkModel linkModel)
        {
            var result = new Result<LinkModel>();
            try
            {
                var link = await _queryRepository.Table<Link>().AsNoTracking().FirstAsync(x => x.Id == linkModel.Id);
                if (link == null)
                {
                    result.Status = ResultStatusEnum.NotFound;
                    result.Message = "The Link not found";
                    return result;
                }

                link.Title = linkModel.Title;
                link.Url = linkModel.Url;
                link.Description = linkModel.Description;
                link.ImagePreviewId = linkModel.ImagePreviewId;
                link.LinkSectionId = linkModel.LinkSectionId;
                link.UserId = linkModel.UserId;




                _commandRepository.Update(link);
                await _commandRepository.SaveChangesAsync();

                result.Data = linkModel;

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
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public async Task<Result<List<LinkModel>>> UpdateOrder(List<LinkModel> linkModelList)
        {
            var result = new Result<List<LinkModel>>();


            var ids = linkModelList.Select(x => x.Id).ToArray();
            var i = linkModelList.Count();
            foreach (var item in linkModelList)
            {
                item.Order = i--;
            }

            var visibleList = _queryRepository.Table<Link>().Where(x => ids.Contains(x.Id)).ToList();
            foreach (var item in visibleList)
            {
                var model = linkModelList.First(x => x.Id == item.Id);
                item.Order = model.Order;
                _commandRepository.Update(item);
            }

            await _commandRepository.SaveChangesAsync();

            result.Data = linkModelList;
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
            var link = await _queryRepository.Table<Link>().FirstOrDefaultAsync(x => x.Id == id);
            if (link is null)
            {
                result.Status = ResultStatusEnum.NotFound;
                result.Message = "The Link not found";
                return result;
            }

            _commandRepository.Delete(link);

            await _commandRepository.SaveChangesAsync();

            return result;
        }

    }
}