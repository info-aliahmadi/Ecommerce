﻿using Hydra.Infrastructure.Data;

namespace Hydra.Crm.Core.Models.Email
{
    public record EmailInboxFromAddressModel
    {

        /// <summary>
        /// 
        /// </summary>
        public int EmailInboxId { get; set; }


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
