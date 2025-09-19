using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Core
{
    public class JwtOptions : IConfigurableOptions
    {
        public string SigningKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuerSigningKey { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool RequireExpirationTime { get; set; } = true;
    }
}
