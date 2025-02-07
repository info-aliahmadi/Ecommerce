using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class OrderNote : BaseEntity<int>
{
    public string Note { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public virtual Order Order { get; set; }

    public virtual User User { get; set; }
}