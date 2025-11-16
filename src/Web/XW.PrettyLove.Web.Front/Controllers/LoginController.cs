using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Domain;

namespace XW.PrettyLove.Web.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class LoginController : ControllerBase
    {
        private readonly IMemberAppService memberAppService;

        public LoginController(IMemberAppService memberAppService)
        {
            this.memberAppService = memberAppService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code">微信客户端临时代码</param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Member> Login(WechatLoginRequestDTO requestDot)
        {
            return await memberAppService.Login(requestDot);
        }
    }
}
