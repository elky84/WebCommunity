using WebShared.Common;

namespace Web.Types
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum AccountGradeType
    {
        [Description("관리자")]
        Admin,

        [Description("중재자")]
        Moderator,

        [Description("개발자")]
        Developer,

        [Description("사용자")]
        User
    }
}
