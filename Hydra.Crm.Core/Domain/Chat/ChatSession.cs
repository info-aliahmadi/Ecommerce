using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Crm.Core.Domain.Chat
{
    public class ChatSession : BaseEntity<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public User ToUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GuestName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GuestEmail { get; set; }


    }

}
