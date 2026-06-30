using Hydra.Cms.Core.Models;
using Hydra.Kernel.GeneralModels;


namespace Hydra.Cms.Core.Interfaces
{
    public interface ISlideshowService
    {
        /// <summary>
        /// Asynchronously retrieves a list of all published slideshows.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> object
        /// with a list of <see cref="SlideshowModel"/> instances representing the published slideshows. The list is
        /// empty if no published slideshows are found.</returns>
        Task<Result<List<SlideshowModel>>> GetPublishedSlideshow();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<SlideshowModel>>> GetList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<SlideshowModel>> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        Task<Result<SlideshowModel>> Add(SlideshowModel slideshowModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slideshowModel"></param>
        /// <returns></returns>
        Task<Result<SlideshowModel>> Update(SlideshowModel slideshowModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slideshowModelList"></param>
        /// <returns></returns>
        Task<Result<List<SlideshowModel>>> UpdateOrder(List<SlideshowModel> slideshowModelList);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Visible(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);


    }
}
