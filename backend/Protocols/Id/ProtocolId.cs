namespace Protocols.Id
{
    public partial class ProtocolId
    : EzAspDotNet.Protocols.Id.ProtocolId
    {
        // Auth
        public static readonly ProtocolId Authenticate = new ProtocolId(1, nameof(Authenticate));
        public static readonly ProtocolId AuthCheck = new ProtocolId(2, nameof(AuthCheck));

        public static readonly ProtocolId SignUp = new ProtocolId(10, nameof(SignUp));
        public static readonly ProtocolId SignIn = new ProtocolId(11, nameof(SignIn));
        public static readonly ProtocolId SignOut = new ProtocolId(12, nameof(SignOut));
        public static readonly ProtocolId AccountUpdate = new ProtocolId(13, nameof(AccountUpdate));

        // Board
        public static readonly ProtocolId Article = new ProtocolId(20, nameof(Article));
        public static readonly ProtocolId ArticleList = new ProtocolId(21, nameof(ArticleList));
        public static readonly ProtocolId Comment = new ProtocolId(22, nameof(Comment));
        public static readonly ProtocolId CommentList = new ProtocolId(23, nameof(CommentList));

        public ProtocolId(int id, string name)
            : base(id, name)
        {
        }
    }
}
