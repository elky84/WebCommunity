using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebUtil.Auth
{
    public static class Extend
    {
        public static string ExtractUserId(this ControllerBase controller)
        {
            if (false == controller.Request.Headers.TryGetValue(WebUtil.Constants.HeaderKeys.AuthorizedUserId, out var userId))
            {
                // TODO throw 
                return null;
            }

            return userId;
        }
    }
}
