using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrettyLove.Core
{
    /// <summary>
    /// JWT帮助类
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// 有效期
        /// </summary>
        private static readonly int DEFAULT_EXPIRE_DAY = 1;

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GenerateToken(List<Claim> claims, Core.JwtOptions options)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(DEFAULT_EXPIRE_DAY),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// 解析JWT
        /// </summary>
        /// <param name="token"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="signingKey"></param>
        /// <returns></returns>
        public static ClaimsPrincipal ValidateToken(string token, string issuer, string audience, string signingKey)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ClockSkew = TimeSpan.FromSeconds(30),//允许服务端时间偏移30秒
                RequireExpirationTime = true
            };
            var principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return principal;
        }


        /// <summary>
        /// 配置JWT
        /// </summary>
        /// <param name="services"></param>
        /// <param name="signingKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJwtBearer(this IServiceCollection services, string signingKey, string issuer, string audience)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    ClockSkew = TimeSpan.FromSeconds(30),//允许服务端时间偏移30秒
                    RequireExpirationTime = true
                };
            });
            return services;
        }

        /// <summary>
        /// 配置JWT
        /// </summary>
        /// <param name="services"></param>
        /// <param name="jwtOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJwtBearer(this IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                    ClockSkew = TimeSpan.FromSeconds(30),//允许服务端时间偏移30秒
                    RequireExpirationTime = true
                };
            });
            return services;
        }
    }
}
