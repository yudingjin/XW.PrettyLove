using Microsoft.AspNetCore.Mvc;
using PrettyLove.Application;
using System.Threading.Tasks;

namespace PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsController : BaseController
    {
        private readonly ISmsAppService service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public SmsController(ISmsAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/sms/send")]
        public async Task<SmsResponseDTO> SendSmsAsync([FromBody] SmsRequestDTO request)
        {
            return await service.SendSmsAsync(request.Mobile, request.Code);
        }
    }
}
