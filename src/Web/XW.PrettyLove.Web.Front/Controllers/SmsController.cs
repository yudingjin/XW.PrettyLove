using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XW.PrettyLove.Application;

namespace XW.PrettyLove.Web.Front.Controllers
{
    [AllowAnonymous]
    public class SmsController : BaseController
    {
        private readonly ISmsAppService service;

        public SmsController(ISmsAppService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<SmsResponseDTO> SendSmsAsync(string phone, string content)
        {
            return await service.SendSmsAsync(phone, content);
        }
    }
}
