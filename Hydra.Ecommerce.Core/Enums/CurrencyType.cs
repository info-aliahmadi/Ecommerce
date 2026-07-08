using System.ComponentModel;

namespace Hydra.Ecommerce.Core.Enums
{

    /// <summary>
    /// Represents a Currency Type
    /// </summary>
    public enum CurrencyType
    {
        [Description("None")]
        None = 0,
        [Description("IRR")]
        Rial = 1,
        [Description("IRT")]
        Toman = 2,
        [Description("IQD")]
        Dinar = 3,
        [Description("USD")]
        Dollar = 4,
        [Description("EUR")]
        Euro = 5
    }
}