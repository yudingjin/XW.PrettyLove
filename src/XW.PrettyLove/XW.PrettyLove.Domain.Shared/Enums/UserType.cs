using System.ComponentModel;

namespace XW.PrettyLove.Domain.Shared
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        SuperAdmin = 99,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("管理员")]
        Admin = 98,

        /// <summary>
        /// 管理员
        /// </summary>
        [Description("AET")]
        AET = 91,

        /// <summary>
        /// 普通账号
        /// </summary>
        [Description("普通账号")]
        Normal = 0
    }
}
