using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebUtil.Auth
{
    public static class Extend
    {
        public static string ExtractUserId(this ControllerBase controller)
        {
            if (false == controller.Request.Headers.TryGetValue(EzAspDotNet.Constants.HeaderKeys.AuthorizedUserId, out var userId))
            {
                Log.Logger.Error($"ExtractUserId() Failed. <RemoteIp:{controller.Request.HttpContext.Connection.RemoteIpAddress}, " +
                    $"LocalIp:{controller.Request.HttpContext.Connection.LocalIpAddress}>");
                return null;
            }

            return userId;
        }
    }
}
