namespace XW.PrettyLove.Domain.Shared
{
    public enum SmsStatus
    {
        Pending,     // 待发送
        Sending,     // 发送中
        Sent,        // 已发送
        Delivered,   // 已送达
        Failed,      // 发送失败
        Canceled     // 已取消
    }
}
