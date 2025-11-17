namespace XW.PrettyLove.Application
{
    public interface ISmsAppService
    {
        Task<bool> SendSmsAsync(string phone, string content);
    }
}
