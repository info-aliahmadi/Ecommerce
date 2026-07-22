using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class Address : BaseEntity<int>
{
    public int UserId { get; set; }
    public string Title { get; set; }

    public int CountryId { get; set; }

    public int StateProvinceId { get; set; }

    public string City { get; set; }

    public string County { get; set; }

    public string ZipPostalCode { get; set; }

    public string Address1 { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// GPS location
    /// </summary>
    public string GeoLocation { get; set; }

    /// <summary>
    /// default address
    /// </summary>
    public bool IsDefault { get; set; }

    public virtual Country Country { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual StateProvince StateProvince { get; set; }

    public virtual User User { get; set; }
}