using System.ComponentModel.DataAnnotations;

namespace Hydra.Crm.Core.Models.Subscribe
{
    public class UserSubscribeModel
    {
        /// <summary>
        /// 
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? SubscribeLabelId { get; set; }

        
    }
}