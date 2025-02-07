using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class ProductReviewHelpfulness : BaseEntity<int>
{
    public int UserId { get; set; }

    public int ProductReviewId { get; set; }

    public bool WasHelpful { get; set; }

    public virtual ProductReview ProductReview { get; set; }

    public virtual User User { get; set; }
}