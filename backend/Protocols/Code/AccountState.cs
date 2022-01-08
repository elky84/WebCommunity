using EnumExtend;
using EzAspDotNet.Common;

namespace Protocols.Code
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum AccountState
    {
        [Description("활성화")]
        Enable,

        [Description("비활성화")]
        Disable,
    }
}
