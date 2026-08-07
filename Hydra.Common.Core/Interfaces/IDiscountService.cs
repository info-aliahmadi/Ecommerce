using Hydra.Kernel.GeneralModels;
using Hydra.Common.Core.Models;

namespace Hydra.Common.Core.Interfaces
{
    public interface IDiscountService
    {

        /// <summary>
        /// Retrieves the discount associated with the specified coupon code.
        /// </summary>
        /// <param name="couponCode">The coupon code to search for. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with the
        /// discount information if the coupon code is valid; otherwise, an error result indicating the reason for
        /// failure.</returns>
        Task<Result<DiscountModel>> GetDiscountByCouponCode(int userId, string couponCode);
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        Task<Result<PaginatedList<DiscountModel>>> GetList(GridDataBound dataGrid);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<DiscountModel>>> GetListForSelect();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<DiscountModel>> GetById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="discountModel"></param>
        /// <returns></returns>
        Task<Result<DiscountModel>> Add(DiscountModel discountModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="discountModel"></param>
        /// <returns></returns>
        Task<Result<DiscountModel>> Update(DiscountModel discountModel);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> Delete(int id);

    }
}