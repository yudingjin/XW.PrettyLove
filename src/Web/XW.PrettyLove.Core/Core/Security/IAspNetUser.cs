using System.Security.Claims;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 登录用户接口
    /// </summary>
    public interface IAspNetUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        long ID { get; }

        /// <summary>
        /// 用户名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 租户ID
        /// </summary>
        long TenantId { get; }

        /// <summary>
        /// 租户编码
        /// </summary>
        string TenantCode { get; }

        /// <summary>
        /// 是否认证
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Claim> GetClaimsIdentity();

        /// <summary>
        /// 获取Claim值
        /// </summary>
        /// <param name="ClaimType"></param>
        /// <returns></returns>
        List<string> GetClaimValueByType(string ClaimType);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        string GetToken();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        List<string> GetUserInfoFromToken(string token);
    }
}
