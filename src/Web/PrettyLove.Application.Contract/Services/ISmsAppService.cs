namespace PrettyLove.Application
{
    public interface ISmsAppService
    {
        Task<SmsResponseDTO> SendSmsAsync(string phone, string content);
    }
}
