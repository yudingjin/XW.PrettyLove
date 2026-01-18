using System.ComponentModel;

namespace PrettyLove.Domain.Shared
{
    /// <summary>
    /// 应用系统
    /// </summary>
    public enum SystemType
    {
        [Description("客户端")]
        Desktop,

        [Description("Android")]
        Android,

        [Description("服务端")]
        Server
    }
}
