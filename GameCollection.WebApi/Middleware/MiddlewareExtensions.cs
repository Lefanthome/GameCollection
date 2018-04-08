using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCollection.WebApi.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, Action<SecurityHeadersOptions> builder)
        {

            SecurityHeadersOptions headersBuilder = new SecurityHeadersOptions();
            if(builder != null)
            {
                builder.Invoke(headersBuilder);
            }
            return app.UseMiddleware<SecurityHeadersMiddleware>(headersBuilder);
        }
    }
}
