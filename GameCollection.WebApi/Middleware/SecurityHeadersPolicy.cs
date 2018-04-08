using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCollection.WebApi.Middleware
{
    public class SecurityHeadersPolicy
    {
        public Dictionary<string, string> AddHeaders { get; } = new Dictionary<string, string>();
        public HashSet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}
