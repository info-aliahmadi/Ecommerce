﻿using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Crm.Core.Domain.Chat
{
    public class ChatUser : BaseEntity<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? UserId { get; set; }

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
