using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Domain;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 账户
    /// </summary>
    [ApiController]
    [EnableCors()]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMemberAppService memberAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberAppService"></param>
        public AccountController(IMemberAppService memberAppService)
        {
            this.memberAppService = memberAppService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/account/login")]
        public async Task<Member> LoginAsync(WechatLoginRequestDTO request)
        {
            return await memberAppService.Login(request);
        }
    }
}
