using Microsoft.AspNetCore.Http;

namespace Auth.Extend
{
    public static class HttpExtend
    {
        public static void TokenSaveToCookie(this HttpResponse response, string token)
        {
            response.Cookies.Delete(token);

            var options = new CookieOptions
            {
#if !DEBUG
                Expires = DateTime.Now.AddMinutes(60),
#endif
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true,
            };

            response.Cookies.Append("Token", token, options);
        }
    }
}
