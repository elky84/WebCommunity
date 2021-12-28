using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Protocols;
using EzAspDotNet.Protocols;

namespace WebUtil.Filter
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var message = string.Join(" | ", context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(new ResponseHeader { ResultCode = Protocols.Code.ResultCode.BadRequest, ErrorMessage = message });
            }
        }
    }
}
