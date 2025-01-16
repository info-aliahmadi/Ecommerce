﻿using Hydra.Infrastructure.Security.Domain;
using Hydra.Infrastructure.Data;

namespace Hydra.Sale.Core.Domain;

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