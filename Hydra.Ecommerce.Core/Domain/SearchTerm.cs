using Hydra.Kernel.Data;

namespace Hydra.Ecommerce.Core.Domain;

public class SearchTerm : BaseEntity<int>
{
    public string Keyword { get; set; }

    public int Count { get; set; }
}