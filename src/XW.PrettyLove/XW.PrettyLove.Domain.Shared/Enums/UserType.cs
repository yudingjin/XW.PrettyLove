using System.ComponentModel;

namespace LS.ERP.Domain.Shared
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
        Normal = 0,

        /// <summary>
        /// 专家
        /// </summary>
        [Description("专家")]
        Professional = 1,

        /// <summary>
        /// 医生
        /// </summary>
        [Description("医生")]
        Doctor = 2,

        /// <summary>
        /// 质控
        /// </summary>
        [Description("质控")]
        QA = 3,

        /// <summary>
        /// 外部医生
        /// </summary>
        [Description("外部医生")]
        ExternalDoctor = 11,

        /// <summary>
        /// 客户用户
        /// </summary>
        [Description("客户用户")]
        CustomerUser = 12
    }
}
