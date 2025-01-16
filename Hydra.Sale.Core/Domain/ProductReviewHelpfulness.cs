﻿using Hydra.Infrastructure.Security.Domain;
using Hydra.Infrastructure.Data;

namespace Hydra.Sale.Core.Domain;

public class ProductReviewHelpfulness : BaseEntity<int>
{
    public int UserId { get; set; }

    public int ProductReviewId { get; set; }

    public bool WasHelpful { get; set; }

    public virtual ProductReview ProductReview { get; set; }

    public virtual User User { get; set; }
}