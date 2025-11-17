using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Core
{
    public class JwtOptions : IConfigurableOptions
    {
        /// <summary>
        /// JWT 密钥
        /// </summary>
        public string SigningKey { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 是否验证SecurityKey
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = true;

        /// <summary>
        /// 是否验证发行人
        /// </summary>
        public bool ValidateIssuer { get; set; } = true;

        /// <summary>
        /// 是否验证订阅人
        /// </summary>
        public bool ValidateAudience { get; set; } = true;

        /// <summary>
        /// 是否验证过期时间
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;

        /// <summary>
        /// 是否验证Token
        /// </summary>
        public bool RequireExpirationTime { get; set; } = true;
    }
}
