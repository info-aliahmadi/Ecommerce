
namespace Hydra.Order.Core.Models
{
    public class OrderItemsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<OrderItemModel> OrderItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SumOrderItemsModel OrderSummary { get; set; }

    }
}