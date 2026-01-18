using FreeSql.DataAnnotations;
using PrettyLove.Domain.Shared;
using PrettyLove.Domain.Wechat;

namespace PrettyLove.Domain
{
    /// <summary>
    /// 微信会员表
    /// </summary>
    [Table(Name = "member")]
    public class Member : WechatEntity
    {
        /// <summary>
        /// 微信OpenId
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像URL
        /// </summary>
        [Column(IsNullable = false, StringLength = 200)]
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// 性别 0-未知 1-男 2-女
        /// </summary>
        [Column(IsNullable = false)]
        public Gender? Gender { get; set; } = 0;

        /// <summary>
        /// 国家
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 省份
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 城市
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 手机号码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// SessionKey
        /// </summary>
        [Column(IsIgnore = true)]
        public string SessionKey { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [Column(IsIgnore = true)]
        public string Token { get; set; }
    }
}
