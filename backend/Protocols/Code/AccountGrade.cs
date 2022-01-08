using EnumExtend;
using EzAspDotNet.Common;

namespace Protocols.Code
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum AccountGrade
    {
        [Description("관리자")]
        Admin,

        [Description("중재자")]
        Moderator,

        [Description("사용자")]
        User
    }
}
