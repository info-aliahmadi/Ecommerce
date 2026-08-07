using Hydra.Ecommerce.Core.Enums;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class Discount : BaseEntity<int>
{
    public string Name { get; set; }

    public string CouponCode { get; set; }

    public string? AdminComment { get; set; }

    public DiscountType DiscountTypeId { get; set; }

    public bool UsePercentage { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? MaximumDiscountAmount { get; set; }

    public DateTime? StartDateUtc { get; set; }

    public DateTime? EndDateUtc { get; set; }

    public bool RequiresCouponCode { get; set; }

    public DiscountLimitationType DiscountLimitationId { get; set; }

    public int? LimitationTimes { get; set; }

    public int? MaximumDiscountedQuantity { get; set; }

    public bool IsActive { get; set; }

    /// <summary>
    /// if assigned to amount of order
    /// </summary>
    public decimal? OrderTotal { get; set; }

    public virtual ICollection<OrderDiscount> OrderDiscounts { get; set; } = new List<OrderDiscount>();

    public virtual ICollection<DiscountCategory> DiscountCategories { get; set; } = new List<DiscountCategory>();

    public virtual ICollection<DiscountManufacturer> DiscountManufacturers { get; set; } = new List<DiscountManufacturer>();

    public virtual ICollection<DiscountProduct> DiscountProducts { get; set; } = new List<DiscountProduct>();
}
