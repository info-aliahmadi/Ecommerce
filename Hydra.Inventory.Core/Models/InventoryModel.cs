﻿using Hydra.Ecommerce.Core.Domain;

namespace Hydra.Inventory.Core.Models
{
    public class InventoryModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AttributeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? AttributeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StockType StockType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReservedQuantity { get; set; }


    }
}