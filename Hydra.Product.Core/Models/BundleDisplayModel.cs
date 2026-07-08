namespace Hydra.Product.Core.Models
{
    public class BundleDisplayModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool ShowOnHomepage { get; set; }
        public int ProductsCount { get; set; }
        public List<ProductBundleModel> ProductIds { get; set; } = new();
    }
}
