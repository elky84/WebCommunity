namespace Protocols.Code
{
    public class ResultCode : EzAspDotNet.Protocols.Code.ResultCode
    {
        public ResultCode(int id, string name) : base(id, name)
        {
        }

        public static ResultCode Fail = new ResultCode(10002, "실패");
        public static ResultCode ProtocolNotMatched = new ResultCode(10005, "프로토콜이 맞지 않습니다");
        public static ResultCode ValidationFailed = new ResultCode(10006, "파라미터 검증 실패");
        public static ResultCode InvalidToken = new ResultCode(10007, "유효하지 않은 토큰");
        public static ResultCode NotPrivodedToken = new ResultCode(10008, "토큰이 전달되지 않았다");
        public static ResultCode NotFoundAccount = new ResultCode(10009, "계정을 찾지 못했다");
        public static ResultCode NotMatchPassword = new ResultCode(10010, "비밀번호가 틀렸다");
        public static ResultCode AlreadyTakenUserId = new ResultCode(10011, "이미 사용된 UserId 입니다");
        public static ResultCode InvalidUserId = new ResultCode(10012, "UserId가 규칙에 위배되었다");
        public static ResultCode NotUsableAccount = new ResultCode(10014, "사용 불가능 한 계정이다");
        public static ResultCode DisconnectByServer = new ResultCode(10015, "서버로부터 연결해제 되었다");
        public static ResultCode AlreadyAuth = new ResultCode(10016, "이미 인증을 받은 상태이다");
        public static ResultCode NotFoundAuthRoutes = new ResultCode(10017, "인증 서버를 찾지 못했다");
        public static ResultCode NotMatchedAuthor = new ResultCode(10019, "해당 데이터의 소유자가 아닙니다");
        public static ResultCode NotFoundData = new ResultCode(10020, "자료를 찾지 못했습니다");
        public static ResultCode DatabaseUpdateFailure = new ResultCode(10021, "데이터베이스 갱신에 실패했습니다");
        public static ResultCode NotPrivodeToken = new ResultCode(10022, "로그인이 필요합니다");
        public static ResultCode NotEditComment = new ResultCode(10023, "편집이 불가능한 댓글입니다");
    }
}
