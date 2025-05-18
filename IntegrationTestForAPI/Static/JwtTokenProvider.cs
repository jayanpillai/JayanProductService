using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTestForAPI.Static
{
    public static class JwtTokenProvider
    {
        public static string Issuer { get; } = "Sample_Auth_Server";
        public static SecurityKey SecurityKey { get; } =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("SecretKeyForTestingJayanPillaiSecretKeyForTestingJayanPillai")
            );
        public static SigningCredentials SigningCredentials { get; } =
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        internal static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    }
}
