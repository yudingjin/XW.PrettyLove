using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using XW.PrettyLove.Application;
using XW.PrettyLove.Core;
using XW.PrettyLove.Domain;
using XW.PrettyLove.Domain.Shared;
using Yitter.IdGenerator;

namespace XW.PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 子女
    /// </summary>
    [Route("api/children")]
    public class ChildrenController : BaseController
    {
        private readonly IAppService<MemberChildren> appService;
        private readonly IMemberAppService memberAppService;
        private readonly IAspNetUser aspNetUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appService"></param>
        /// <param name="aspNetUser"></param>
        public ChildrenController(IAppService<MemberChildren> appService, IMemberAppService memberAppService, IAspNetUser aspNetUser)
        {
            this.appService = appService;
            this.memberAppService = memberAppService;
            this.aspNetUser = aspNetUser;
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
        /// 获取子女详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/children/get")]
        public async Task<ChildrenFormFormDTO> DetailAsync()
        {
            var memberId = 11;
            var dataInfo = await memberAppService.GetChildrenAsync(memberId);
            if (dataInfo.Item1 == null)
                throw new FriendlyException("数据不存在", HttpStatusCode.NotFound);
            return new ChildrenFormFormDTO(dataInfo.Item1.Adapt<ChildrenDTO>(), dataInfo.Item2.Adapt<ChildrenConditionDTO>(), dataInfo.Item3.Adapt<List<ChildrenImageDTO>>());
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/children/save")]
        public ChildrenFormFormDTO Save([FromBody] ChildrenFormFormDTO request)
        {
            if (request == null)
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);
            var child = request.Basic.Adapt<MemberChildren>();
            if (child.Id == null)
            {
                child.Id = YitIdHelper.NextId();
                child.CreatedTime = DateTime.Now;
                child.EntityStatus = EntityStatus.New;
            }
            else
            {
                child.EntityStatus = EntityStatus.Updated;
            }
            var childCondition = request.Condition.Adapt<MemberChildrenCondition>();
            if (childCondition.Id == null)
            {
                childCondition.Id = YitIdHelper.NextId();
                childCondition.ChildId = child.Id;
                childCondition.CreatedTime = DateTime.Now;
                childCondition.EntityStatus = EntityStatus.New;

            }
            else
            {
                childCondition.EntityStatus = EntityStatus.Updated;
            }
            var childImages = request.ImageList.Adapt<List<MemberChildrenImage>>();
            childImages.ForEach(item =>
            {
                item.Id = YitIdHelper.NextId();
                item.CreatedTime = DateTime.Now;
                item.EntityStatus = EntityStatus.New;
                item.ChildId = child.Id;
            });
            var result = memberAppService.Save(child, childCondition, childImages);
            if (!result)
                throw new FriendlyException("保存失败", HttpStatusCode.InternalServerError);
            return new ChildrenFormFormDTO(child.Adapt<ChildrenDTO>(), childCondition.Adapt<ChildrenConditionDTO>(), childImages.Adapt<List<ChildrenImageDTO>>());
        }
    }
}
