using Hydra.Auth.Domain;
using Hydra.Kernel.Data;
using Hydra.Kernel.GeneralModels;


namespace Hydra.Crm.Core.Models.Message
{
    public record MessageUserModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AuthorModel ToUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsPin { get; set; }

    }

}
