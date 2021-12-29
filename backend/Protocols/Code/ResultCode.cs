namespace Protocols.Code
{
    public class ResultCode : EzAspDotNet.Code.ResultCode
    {
        public ResultCode(int id, string name) : base(id, name)
        {
        }

        public static ResultCode Fail = new(10002, "실패");
        public static ResultCode ProtocolNotMatched = new(10005, "프로토콜이 맞지 않습니다");
        public static ResultCode ValidationFailed = new(10006, "파라미터 검증 실패");
        public static ResultCode InvalidToken = new(10007, "유효하지 않은 토큰");
        public static ResultCode NotPrivodedToken = new(10008, "토큰이 전달되지 않았다");
        public static ResultCode NotFoundAccount = new(10009, "계정을 찾지 못했다");
        public static ResultCode NotMatchPassword = new(10010, "비밀번호가 틀렸다");
        public static ResultCode AlreadyTakenUserId = new(10011, "이미 사용된 UserId 입니다");
        public static ResultCode InvalidUserId = new(10012, "UserId가 규칙에 위배되었다");
        public static ResultCode NotUsableAccount = new(10014, "사용 불가능 한 계정이다");
        public static ResultCode DisconnectByServer = new(10015, "서버로부터 연결해제 되었다");
        public static ResultCode AlreadyAuth = new(10016, "이미 인증을 받은 상태이다");
        public static ResultCode NotFoundAuthRoutes = new(10017, "인증 서버를 찾지 못했다");
        public static ResultCode NotMatchedAuthor = new(10019, "해당 데이터의 소유자가 아닙니다");
        public static ResultCode NotFoundData = new(10020, "자료를 찾지 못했습니다");
        public static ResultCode DatabaseUpdateFailure = new(10021, "데이터베이스 갱신에 실패했습니다");
        public static ResultCode NotPrivodeToken = new(10022, "로그인이 필요합니다");
        public static ResultCode NotEditComment = new(10023, "편집이 불가능한 댓글입니다");
    }
}
