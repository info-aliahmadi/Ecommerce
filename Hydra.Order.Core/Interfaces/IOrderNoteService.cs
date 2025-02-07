using Hydra.Kernel.GeneralModels;
using Hydra.Order.Core.Models;

namespace Hydra.Order.Core.Interfaces
{
    public interface IOrderNoteService
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<OrderNoteModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<OrderNoteModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderNoteModel"></param>
        /// <returns></returns>
        Task<Result<OrderNoteModel>> Add(OrderNoteModel orderNoteModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderNoteModel"></param>
        /// <returns></returns>
        Task<Result<OrderNoteModel>> Update(OrderNoteModel orderNoteModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}