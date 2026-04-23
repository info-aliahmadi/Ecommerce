using System.ComponentModel;

namespace Hydra.Kernel.Enums;

public enum CurrencyType
{
    [Description("None")]
    None = 0,
    [Description("IQD")]
    Dinar = 1,
    [Description("IRR")]
    Rial = 2,
    [Description("IRT")]
    Toman = 3,
    [Description("USD")]
    Dollar = 4,
    [Description("EUR")]
    Euro = 5
}