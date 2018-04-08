using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCollection.WebApi.Middleware
{
    public class SecurityHeadersOptions
    {
        public SecurityHeadersPolicy policy { get; private set; } = new SecurityHeadersPolicy();


        public SecurityHeadersOptions AddHeader(string key, string value)
        {
            if(!policy.AddHeaders.ContainsKey(key))
            {
                policy.AddHeaders.Add(key, value);
            }
            return this;
        }

        public SecurityHeadersOptions RemoveHeader(string header)
        {
            policy.RemoveHeaders.Add(header);
            return this;
        }
    }
}
