﻿using Hydra.Infrastructure.Data;

namespace Hydra.Crm.Core.Domain.Email
{
    public class EmailInboxFromAddress : BaseEntity<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public int EmailInboxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmailInbox EmailInbox { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }


    }

}
