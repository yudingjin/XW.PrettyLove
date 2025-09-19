﻿using FreeSql.DataAnnotations;
using XW.PrettyLove.Domain.Wechat;

namespace XW.PrettyLove.Domain
{
    /// <summary>
    /// 微信用户表
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
        /// 微信UnionId
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string UnionId { get; set; } = string.Empty;

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
        public int? Gender { get; set; } = 0;

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
        /// 国家
        /// </summary>
        [Column(IsNullable = false, StringLength = 50)]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// 手机号码
        /// </summary>
        [Column(IsNullable = false, StringLength = 20)]
        public string Phone { get; set; } = string.Empty;
    }
}
