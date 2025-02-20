﻿using Hydra.Ecommerce.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hydra.Ecommerce.Core.EntityConfiguration
{
    public class SearchTermConfiguration : IEntityTypeConfiguration<SearchTerm>
    {
        public void Configure(EntityTypeBuilder<SearchTerm> entity)
        {
            entity.ToTable("SearchTerm", "Sale");

            entity.Property(e => e.Keyword).HasMaxLength(50);
        }
    }
}
