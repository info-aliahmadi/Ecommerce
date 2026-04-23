using Hydra.Kernel.Data;
using Hydra.Kernel.Setting.Enum;

namespace Hydra.Kernel.Setting.Domain
{
    public class SiteSetting : BaseEntity<int>
    {
        public required string Key { get; set; }
        public string? Value { get; set; }
        public SettingValueTypeEnum ValueType { get; set; }

    }
}
