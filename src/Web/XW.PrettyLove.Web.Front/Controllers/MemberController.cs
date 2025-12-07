using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Core;
using XW.PrettyLove.Domain;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 会员
    /// </summary>
    public class MemberController : BaseController
    {
        private readonly IMemberAppService service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public MemberController(IMemberAppService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/member/get")]
        public async Task<Member> GetMemberAsync(string openId)
        {
            var memberInfo = await service.GetAsync(p => p.OpenId == openId);
            if (memberInfo == null)
                throw new FriendlyException("用户不存在", HttpStatusCode.NotFound);
            return memberInfo;
        }
    }
}
