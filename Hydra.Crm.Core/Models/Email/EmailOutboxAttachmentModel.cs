using Hydra.Kernel.Data;

namespace Hydra.Crm.Core.Models.Email
{
    public record EmailOutboxAttachmentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmailOutboxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AttachmentId { get; set; }

    }
}
