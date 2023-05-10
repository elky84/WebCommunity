using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebUtil.Auth
{
    public static class Extend
    {
        public static string ExtractUserId(this ControllerBase controller)
        {
            if (false != controller.Request.Headers.TryGetValue(EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId,
                    out var userId)) return userId;
            Log.Logger.Error("{ExtractUserIdName}() Failed. <RemoteIp:{ConnectionRemoteIpAddress}> <LocalIp:{ConnectionLocalIpAddress}> ", nameof(ExtractUserId), controller.Request.HttpContext.Connection.RemoteIpAddress, controller.Request.HttpContext.Connection.LocalIpAddress);
            return null;

        }
    }
}
