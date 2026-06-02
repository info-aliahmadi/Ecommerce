using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class TaxRate : BaseEntity<int>
{
    public int CountryId { get; set; }
    public int TaxCategoryId { get; set; }

    public decimal Percentage { get; set; }

    public virtual Country Country { get; set; }
    public virtual TaxCategory TaxCategory { get; set; }
}