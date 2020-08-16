using WebShared.Common;

namespace Web.Types
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum AccountStateType
    {
        [Description("활성화")]
        Enable,

        [Description("비활성화")]
        Disable,

        [Description("삭제")]
        Delete,

        [Description("중지")]
        Suspend
    }
}
