using Microsoft.AspNetCore.Mvc;

namespace WebUtil.Auth
{
    public static class Extend
    {
        public static string ExtractUserId(this ControllerBase controller)
        {
            if (false == controller.Request.Headers.TryGetValue(EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId, out var userId))
            {
                // TODO throw 
                return null;
            }

            return userId;
        }
    }
}
