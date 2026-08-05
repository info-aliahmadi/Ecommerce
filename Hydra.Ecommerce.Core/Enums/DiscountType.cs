namespace Hydra.Ecommerce.Core.Enums
{
    /// <summary>
    /// Represents a discount type
    /// </summary>
    public enum DiscountType
    {
        /// <summary>
        /// Assigned to Coupon Code 
        /// </summary>
        AssignedToCouponCode = 1,
        /// <summary>
        /// Assigned to order total 
        /// </summary>
        AssignedToOrderTotal = 2,

        /// <summary>
        /// Assigned to products
        /// </summary>
        AssignedToProduct = 3,

        /// <summary>
        /// Assigned to categories (all products in a category)
        /// </summary>
        AssignedToCategory = 4,

        /// <summary>
        /// Assigned to manufacturers (all products of a manufacturer)
        /// </summary>
        AssignedToManufacturer = 5,

    }
}