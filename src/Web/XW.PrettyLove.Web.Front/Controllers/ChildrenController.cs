using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Core;
using XW.PrettyLove.Domain;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 子女
    /// </summary>
    [Route("api/children")]
    public class ChildrenController : BaseController
    {
        private readonly IAppService<MemberChildren> appService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appService"></param>
        public ChildrenController(IAppService<MemberChildren> appService)
        {
            this.appService = appService;
        }

        /// <summary>
        /// 分页请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/children/page")]
        public async Task<IPagedList<MemberChildren>> PageAsync([FromBody] ChildrenPagedRequestDTO request)
        {
            var pagedList = await appService.GetPagedListAsync(p => p.Id > 0, request.PageIndex, request.PageSize);
            if (pagedList.Data?.Count <= 0)
                throw new FriendlyException("没有数据", HttpStatusCode.BadRequest);
            return pagedList;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/children/save")]
        public Boolean SaveAsync([FromBody] ChildrenFormRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
