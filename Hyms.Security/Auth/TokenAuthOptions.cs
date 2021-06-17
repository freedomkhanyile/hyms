using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hyms.Security.Auth
{
    public  class TokenAuthOptions
    {
        public static string Audience { get; } = "ExpensesAudience";
        public static string Issuer { get; } = "ExpensesIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
        public static string TokenType { get; } = "Bearer";
    }
}
