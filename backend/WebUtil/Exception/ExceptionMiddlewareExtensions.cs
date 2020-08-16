using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NLog;
using System.Net;
using Web.Protocols.Exception;

namespace WebUtil.Exception
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Logger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        if (contextFeature.Error.GetType() == typeof(LogicException))
                        {
                            var logicException = (LogicException)contextFeature.Error;
                            context.Response.StatusCode = (int)logicException.HttpStatusCode;

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                ErrorMessage = logicException.ResultCode.ToString(),
                                Detail = logicException.Detail,
                                ResultCode = logicException.ResultCode
                            }));
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
                            {
                                StatusCode = context.Response.StatusCode,
                                ErrorMessage = "Internal Server Error",
                                Detail = contextFeature.Error.Message,
                                ResultCode = Web.Code.ResultCode.UnknownException
                            }));
                        }
                    }
                });
            });
        }
    }
}
