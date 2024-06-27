
using Microsoft.AspNetCore.Http;

namespace WeNerds.Middlewares.Extensions;

public static class MiddlewareExtensions
{
        public static bool IsApiRequest(this HttpContext context)
            => context.Request.Path.StartsWithSegments("/api");
}
