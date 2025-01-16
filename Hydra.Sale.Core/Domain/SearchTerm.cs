﻿using Hydra.Infrastructure.Data;

namespace Hydra.Sale.Core.Domain;

public class SearchTerm : BaseEntity<int>
{
    public string Keyword { get; set; }

    public int Count { get; set; }
}