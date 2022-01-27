namespace Protocols.Id
{
    public partial class ProtocolId
    : EzAspDotNet.Protocols.Id.ProtocolId
    {
        // Auth
        public static readonly ProtocolId Authenticate = new(1, nameof(Authenticate));
        public static readonly ProtocolId AuthCheck = new(2, nameof(AuthCheck));

        public static readonly ProtocolId SignUp = new(10, nameof(SignUp));
        public static readonly ProtocolId SignIn = new(11, nameof(SignIn));
        public static readonly ProtocolId SignOut = new(12, nameof(SignOut));
        public static readonly ProtocolId AccountUpdate = new(13, nameof(AccountUpdate));

        // Board
        public static readonly ProtocolId Article = new(20, nameof(Article));
        public static readonly ProtocolId ArticleList = new(21, nameof(ArticleList));
        public static readonly ProtocolId Comment = new(22, nameof(Comment));
        public static readonly ProtocolId CommentList = new(23, nameof(CommentList));

        public ProtocolId(int id, string name)
            : base(id, name)
        {
        }
    }
}
