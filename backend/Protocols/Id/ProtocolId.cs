namespace Protocols.Id
{
    public partial class ProtocolId
    : EzAspDotNet.Protocols.Id.ProtocolId
    {
        // Auth
        public static ProtocolId Authenticate = new(1, nameof(Authenticate));
        public static ProtocolId AuthCheck = new(2, nameof(AuthCheck));

        public static ProtocolId SignUp = new(10, nameof(SignUp));
        public static ProtocolId SignIn = new(11, nameof(SignIn));
        public static ProtocolId SignOut = new(12, nameof(SignOut));
        public static ProtocolId AccountUpdate = new(13, nameof(AccountUpdate));

        // Board
        public static ProtocolId Article = new(20, nameof(Article));
        public static ProtocolId ArticleList = new(21, nameof(ArticleList));
        public static ProtocolId Comment = new(22, nameof(Comment));
        public static ProtocolId CommentList = new(23, nameof(CommentList));

        public ProtocolId(int id, string name)
            : base(id, name)
        {
        }
    }
}
