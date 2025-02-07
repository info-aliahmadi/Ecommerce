using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Crm.Core.Domain.Message
{
    public class MessageAttachment : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public Message Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MessageId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int AttachmentId { get; set; }


    }

}
