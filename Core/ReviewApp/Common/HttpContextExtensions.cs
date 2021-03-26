using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ReviewApp.Common
{
    public static class HttpContextExtensions
    {
        public static bool TryGetHttpHeader(this HttpContext context, string key, out string value)
        {
            value = null;

            if (!string.IsNullOrWhiteSpace(key) && context.Request.Headers.TryGetValue(key, out StringValues values))
            {
                if (!string.IsNullOrWhiteSpace(values))
                {
                    value = values;
                    return true;
                }
            }

            return false;
        }
    }
}
