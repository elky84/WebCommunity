using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Extend
{
    public static class HttpExtend
    {
        public static void TokenSaveToCookie(this HttpResponse response, string token)
        {
            response.Cookies.Delete(token);

            var options = new CookieOptions
            {
                //Expires = DateTime.Now.AddMinutes(60),
                IsEssential = true
            };

            response.Cookies.Append("Token", token, options);
        }
    }
}
