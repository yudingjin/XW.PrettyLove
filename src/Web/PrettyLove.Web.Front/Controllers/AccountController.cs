using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PrettyLove.Application;
using PrettyLove.Core;
using System.Net;
using System.Threading.Tasks;

namespace PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 账户
    /// </summary>
    [ApiController]
    [EnableCors()]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly JwtOptions options;
        private readonly IMemberAppService memberAppService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memberAppService"></param>
        /// <param name="options"></param>
        public AccountController(IMemberAppService memberAppService, IOptions<JwtOptions> options)
        {
            this.memberAppService = memberAppService;
            this.options = options.Value;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/account/login")]
        public async Task<WechatLoginResponseDTO> LoginAsync(WechatLoginRequestDTO request)
        {
            var memberInfo = await memberAppService.LoginAsync(request);
            if (memberInfo == null)
                throw new FriendlyException("登录失败", HttpStatusCode.InternalServerError);
            return new WechatLoginResponseDTO
            {
                OpenId = memberInfo.OpenId,
                SessionKey = memberInfo.SessionKey,
                UserId = memberInfo.Id,
                UserName = memberInfo.NickName,
                Token = memberInfo.GetAccessToken(options)
            };
        }
    }
}
