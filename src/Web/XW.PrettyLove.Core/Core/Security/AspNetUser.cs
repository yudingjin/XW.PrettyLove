using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace XW.PrettyLove.Core
{
    public class AspNetUser : IAspNetUser
    {
        /// <summary>
        /// http上下文
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accessor"></param>
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long ID => Convert.ToInt64(GetClaimValueByType("Id").FirstOrDefault());

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name => GetClaimValueByType("UserName").FirstOrDefault();

        /// <summary>
        /// 是否已认证
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            var token = _accessor.HttpContext?.Request?.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return token;
        }

        /// <summary>
        /// 从Token中取得指定類型的Claim值
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        public List<string> GetUserInfoFromToken(string ClaimType)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            string token = GetToken();
            if (!string.IsNullOrEmpty(token) && jwtHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
                return jwtToken.Claims.Where(p => p.Type == ClaimType).Select(p => p.Value).ToList();
            }
            return new List<string>() { };
        }

        /// <summary>
        /// 取得ClaimsIdentity
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsIdentity()
        {
            if (_accessor.HttpContext == null) return ArraySegment<Claim>.Empty;

            if (!IsAuthenticated()) return GetClaimsIdentity(GetToken());

            var claims = _accessor.HttpContext.User.Claims.ToList();
            var headers = _accessor.HttpContext.Request.Headers;
            foreach (var header in headers)
            {
                claims.Add(new Claim(header.Key, header.Value));
            }
            return claims;
        }

        /// <summary>
        /// 取得指定Token的ClaimsIdentity
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<Claim> GetClaimsIdentity(string token)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(token) && jwtHandler.CanReadToken(token))
            {
                var jwtToken = jwtHandler.ReadJwtToken(token);
                return jwtToken.Claims;
            }
            return new List<Claim>();
        }

        /// <summary>
        /// 取得指定類型的Claim值
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        public List<string> GetClaimValueByType(string ClaimType)
        {
            return GetClaimsIdentity().Where(p => p.Type == ClaimType).Select(p => p.Value).ToList();
        }
    }
}
