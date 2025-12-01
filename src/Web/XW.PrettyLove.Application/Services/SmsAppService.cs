using Flurl.Http;
using Microsoft.Extensions.Configuration;
using XW.PrettyLove.Core;

namespace XW.PrettyLove.Application
{
    [ScopedDependency]
    public class SmsAppService : ISmsAppService
    {
        private readonly IConfiguration configuration;

        public SmsAppService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<SmsResponseDTO> SendSmsAsync(string phone, string content)
        {
            var url = configuration["Sms:RequestUrl"];
            var appId = configuration["Sms:AppId"];
            var appSecret = configuration["Sms:AppSecret"];
            var templateId = configuration["Sms:TemplateId"];
            var httpContent = new StringContent($"appid={appId}&to={phone}&project={templateId}&signature={appSecret}");
            var result = await url.PostAsync(httpContent);
            var response = await result.GetStringAsync();
            return response.ToObject<SmsResponseDTO>();
        }
    }
}
