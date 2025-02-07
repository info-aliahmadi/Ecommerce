using Hydra.Auth.Domain;
using Hydra.Kernel.Data;

namespace Hydra.Cms.Core.Domain
{
    public class LinkSection : BaseEntity<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVisible { get; set; } = true;


        /// <summary>
        /// 
        /// </summary>
        public List<Link> Links { get; set; } = new();
    }
}
