using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Application
{
    /// <summary>
    /// 微信code2Session返回数据
    /// </summary>
    public class WechatLoginRequestDTO
    {
        /// <summary>
        /// 微信登录临时凭证
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// 用户昵称（wx.getUserInfo()获取）
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 用户性别 
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// 用户头像URL
        /// </summary>
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; } = string.Empty;
    }

    public class WechatLoginResponseDTO
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string SessionKey { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

    public class DecryptDTO
    {
        public string EncryptedData { get; set; }

        public string SessionKey { get; set; }

        public string Iv { get; set; }
    }
}
