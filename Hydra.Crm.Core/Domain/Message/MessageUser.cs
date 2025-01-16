﻿using Hydra.Infrastructure.Security.Domain;
using Hydra.Infrastructure.Data;

namespace Hydra.Crm.Core.Domain.Message
{
    public class MessageUser : BaseEntity<int>
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
        public User ToUser { get; set; }
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
