namespace Hydra.Product.Core.Models
{
    public class ProductImageModel
    {
        /// <summary>
        /// File id of the image.
        /// </summary>
        public int ImageId { get; set; }

        /// <summary>
        /// Display order for this image; lower values show first.
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}