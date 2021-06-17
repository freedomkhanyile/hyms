using System;
using System.Collections.Generic;
using System.Text;

namespace Hyms.Security.Auth
{
    public class TokenBuilder : ITokenBuilder
    {
        public string Build(string name, string[] roles, DateTime expireDate)
        {
            throw new NotImplementedException();
        }
    }
}
