using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace GameCollection.WebApi.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecurityHeadersOptions _options;

        public SecurityHeadersMiddleware(RequestDelegate next, SecurityHeadersOptions options)
        {
            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext context)
        {
            var headers = context.Response.Headers;

            foreach(var item in _options.policy.AddHeaders)
            {
                headers.Add(item.Key, item.Value);
            }

            foreach (var header in _options.policy.RemoveHeaders)
            {
                headers.Remove(header);
            }
            return this._next(context);
        }


    }
}
