using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LS.ERP.Infrastructure
{
    public static class JwtExtensions
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJwtBearer(this IServiceCollection services, JwtOptions option)
        {
            var keyByteArray = Encoding.ASCII.GetBytes(option.SigningKey);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = option.Issuer,//发行人
                ValidateAudience = true,
                ValidAudience = option.Audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                    {
                        //不使用https
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = false;
                        opt.TokenValidationParameters = tokenValidationParameters;
                    });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <param name="secret"></param>
        /// <param name="defaultScheme"></param>
        /// <param name="isHttps"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomJwtBearer(this IServiceCollection services, string issuer, string audience, string secret, string defaultScheme, bool isHttps = false)
        {
            var keyByteArray = Encoding.ASCII.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,            //是否验证SecurityKey
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,           //是否验证Issuer
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,        //是否验证Audience
                ValidAudience = audience,//订阅人
                ValidateLifetime = true,          // 是否验证失效时间
                ClockSkew = TimeSpan.FromSeconds(30),             //过期时间容错值，解决服务器端时间不同步问题（秒）
                RequireExpirationTime = true,
            };
            services.AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(defaultScheme, opt =>
                    {
                        //不使用https
                        opt.SaveToken = true;
                        opt.RequireHttpsMetadata = isHttps;
                        opt.TokenValidationParameters = tokenValidationParameters;
                    });
            return services;
        }
    }
}
