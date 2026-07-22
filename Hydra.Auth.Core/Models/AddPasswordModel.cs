using System.ComponentModel.DataAnnotations;

namespace Hydra.Auth.Models
{
    public class AddPasswordModel
    {
        [Required]
        public string Email { get; set; }

        //[DataType(DataType.Password)]
        //[StringLength(6, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
