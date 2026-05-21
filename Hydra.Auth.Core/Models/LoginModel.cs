using System.ComponentModel.DataAnnotations;

namespace Hydra.Auth.Models
{
    public record LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
