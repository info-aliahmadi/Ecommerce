using Hydra.Ecommerce.Core.Domain;
using Hydra.Kernel.GeneralModels;

namespace Hydra.Product.Core.Models
{
    public class ProductFilterDisplayModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Sort? Sorting { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? SearchInput { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? HasStockQuantity { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? TopRate { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? TopSell { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal? FromSellUnitPrice { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal? ToSellUnitPrice { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? FromAvailableStartDateTimeUtc { get; set; } = null;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime? ToAvailableStartDateTimeUtc { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? HasDiscounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int>? CategoryIds { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AttributeType>? AttributeTypes { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int>? ManufacturerIds { get; set; } = new();

    }

}