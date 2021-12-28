namespace Protocols.Code
{
    public class ResultCode : EzAspDotNet.Code.ResultCode
    {
        public ResultCode(int id, string name) : base(id, name)
        {
        }

        public static ResultCode Fail = new(2, "실패");
        public static ResultCode HttpError = new(3, "Http오류");
        public static ResultCode BadRequest = new(4, "잘못된 요청");
        public static ResultCode ProtocolNotMatched = new(5, "프로토콜이 맞지 않습니다");
        public static ResultCode ValidationFailed = new(6, "파라미터 검증 실패");
        public static ResultCode InvalidToken = new(7, "유효하지 않은 토큰");
        public static ResultCode NotPrivodedToken = new(8, "토큰이 전달되지 않았다");
        public static ResultCode NotFoundAccount = new(9, "계정을 찾지 못했다");
        public static ResultCode NotMatchPassword = new(10, "비밀번호가 틀렸다");
        public static ResultCode AlreadyTakenUserId = new(11, "이미 사용된 UserId 입니다");
        public static ResultCode InvalidUserId = new(12, "UserId가 규칙에 위배되었다");
        public static ResultCode UnknownException = new(13, "핸들링하지 못하는 예외가 발생했다");
        public static ResultCode NotUsableAccount = new(14, "사용 불가능 한 계정이다");
        public static ResultCode DisconnectByServer = new(15, "서버로부터 연결해제 되었다");
        public static ResultCode AlreadyAuth = new(16, "이미 인증을 받은 상태이다");
        public static ResultCode NotFoundAuthRoutes = new(17, "인증 서버를 찾지 못했다");
        public static ResultCode NotConnectedMQ = new(18, "MQ에 연결되지 않았습니다");
        public static ResultCode NotMatchedAuthor = new(19, "해당 데이터의 소유자가 아닙니다");
        public static ResultCode NotFoundData = new(20, "자료를 찾지 못했습니다");
        public static ResultCode DatabaseUpdateFailure = new(21, "데이터베이스 갱신에 실패했습니다");
        public static ResultCode NotPrivodeToken = new(22, "로그인이 필요합니다");
        public static ResultCode NotEditComment = new(23, "편집이 불가능한 댓글입니다");
    }
}
