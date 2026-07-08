namespace Hydra.Product.Core.Models
{
    public class BundleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool ShowOnHomepage { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        ///  any bundle have multiple product(with displayorder) wich is saved in productbundle
        /// </summary>
        public List<ProductBundleModel> ProductIds { get; set; } = new();
    }
}
