using Hydra.Kernel.GeneralModels;

namespace Hydra.Product.Core.Models
{
    public class ProductReviewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AuthorModel User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ProductId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsApproved { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReviewText { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ReplyText { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CustomerNotifiedOfReply { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Rating { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime CreatedOnUtc { get; set; }

    }
}