using Hydra.Kernel.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hydra.Auth.Domain
{
    public class User : IdentityUser<int>
    {
        [PersonalData]
        public string? Name { get; set; }

        //[PersonalData]
        //public DateTime DOB { get; set; }

        [PersonalData]
        public DateTime? RegisterDate { get; set; }

        [PersonalData]
        public LanguageType? DefaultLanguage { get; set; } = LanguageType.English;

        [PersonalData]
        public ThemeType? DefaultTheme { get; set; } = ThemeType.Light;

        [PersonalData]
        public string? Avatar { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

    }
}
