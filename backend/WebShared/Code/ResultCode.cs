using WebShared.Common;

namespace Web.Code
{
    [DescriptiveEnumEnforcement(DescriptiveEnumEnforcementAttribute.EnforcementTypeEnum.ThrowException)]
    public enum ResultCode
    {
        [Description("성공")]
        Success,

        [Description("실패")]
        Fail,

        [Description("Http오류")]
        HttpError,

        [Description("잘못된 요청")]
        BadRequest,

        [Description("프로토콜이 맞지 않습니다")]
        ProtocolNotMatched,

        [Description("파라미터 검증 실패")]
        ValidationFailed,

        [Description("유효하지 않은 토큰")]
        InvalidToken,

        [Description("토큰이 전달되지 않았다")]
        NotPrivodedToken,

        [Description("계정을 찾지 못했다")]
        NotFoundAccount,

        [Description("비밀번호가 틀렸다")]
        NotMatchPassword,

        [Description("이미 사용된 UserId 입니다")]
        AlreadyTakenUserId,

        [Description("UserId가 규칙에 위배되었다")]
        InvalidUserId,

        [Description("핸들링하지 못하는 예외가 발생했다")]
        UnknownException,

        [Description("사용 불가능 한 계정이다")]
        NotUsableAccount,

        [Description("서버로부터 연결해제 되었다")]
        DisconnectByServer,

        [Description("이미 인증을 받은 상태이다")]
        AlreadyAuth,

        [Description("인증 서버를 찾지 못했다")]
        NotFoundAuthRoutes,

        [Description("MQ에 연결되지 않았습니다")]
        NotConnectedMQ,

        [Description("해당 데이터의 소유자가 아닙니다")]
        NotMatchedAuthor,

        [Description("자료를 찾지 못했습니다")]
        NotFoundData,

        [Description("데이터베이스 갱신에 실패했습니다")]
        DatabaseUpdateFailure,

        [Description("로그인이 필요합니다")]
        NotPrivodeToken,
    }
}
