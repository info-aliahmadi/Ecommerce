﻿using Hydra.Infrastructure.Data;

namespace Hydra.Crm.Core.Domain.Email
{
    public class EmailOutboxToAddress : BaseEntity<int>
    {

        /// <summary>
        /// 
        /// </summary>
        public int EmailOutboxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EmailOutbox EmailOutbox { get; set; }

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
