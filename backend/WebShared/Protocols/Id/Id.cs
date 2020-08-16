﻿namespace Web.Protocols
{
    public enum Id
    {
        InvalidId,

        // Auth Start
        Authenticate,
        AuthCheck,
        WebSocketAuth,
        // Auth End

        // Board Start
        Article,
        ArticleList,
        Comment,
        CommentList,
        // Board End

        // Admin Start
        AdminAccountSearch,
        AdminDisconnectUser,
        // Admin End
    }
}
