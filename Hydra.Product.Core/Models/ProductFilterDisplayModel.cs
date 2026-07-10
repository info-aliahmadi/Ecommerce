using Hydra.Ecommerce.Core.Domain;
using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Enums;

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
        public SortingType? Sorting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string? SearchInput { get; set; } = null;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int>? ProductTagIds { get; set; } = new();


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
        public bool? HasDiscounts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool? HasStockQuantity { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateFilter? DateFilter { get; set; }


    }

}