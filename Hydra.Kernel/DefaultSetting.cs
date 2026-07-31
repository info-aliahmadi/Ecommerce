using Hydra.Kernel.Enums;

namespace Hydra.Kernel
{
    public static class DefaultSetting
    {
        public const CurrencyType DEFAULT_CURRENCY = CurrencyType.Dollar;
        public const LanguageType DEFAULT_LANGUAGE = LanguageType.English; // en | ar | fa
        public const ThemeType DEFAULT_THEME = ThemeType.Light; // dark | light
        public const int DEFAULT_COUNTRY = 100;
        public const int DEFAULT_SUBSCRIBE_LABEL = 1;
        public const bool AUTO_APPROVE_REVIEW = true;

    }
}
