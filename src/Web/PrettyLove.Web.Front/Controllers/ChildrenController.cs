using Mapster;
using Microsoft.AspNetCore.Mvc;
using PrettyLove.Application;
using PrettyLove.Core;
using PrettyLove.Domain;
using PrettyLove.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace PrettyLove.Web.Front.Controllers
{
    /// <summary>
    /// 子女
    /// </summary>
    [Route("api/children")]
    public class ChildrenController : BaseController
    {
        private readonly IGenericRepository<MemberChildren> childRepository;
        private readonly IGenericRepository<MemberChildrenCondition> conditionRepository;
        private readonly IMemberAppService memberAppService;
        private readonly IAspNetUser aspNetUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="childRepository"></param>
        /// <param name="conditionRepository"></param>
        /// <param name="memberAppService"></param>
        /// <param name="aspNetUser"></param>
        public ChildrenController
        (
            IGenericRepository<MemberChildren> childRepository,
            IGenericRepository<MemberChildrenCondition> conditionRepository,
            IMemberAppService memberAppService,
            IAspNetUser aspNetUser
        )
        {
            this.childRepository = childRepository;
            this.conditionRepository = conditionRepository;
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
            var pagedList = await memberAppService.GetPagedListAsync(request.Gender, request.MinAge, request.MaxAge, request.MinHeight, request.MaxHeight, request.PageIndex, request.PageIndex);
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
            var memberId = aspNetUser.ID;
            var dataInfo = await memberAppService.GetChildrenAsync(memberId);
            if (dataInfo.Item1 == null)
                throw new FriendlyException("数据不存在", HttpStatusCode.NotFound);
            return new ChildrenFormFormDTO(dataInfo.Item1.Adapt<ChildrenDTO>(), dataInfo.Item2.Adapt<ChildrenConditionDTO>(), dataInfo.Item3.Adapt<List<ChildrenImageDTO>>());
        }

        /// <summary>
        /// 获取子女详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/children/{memberId}")]
        public async Task<ChildrenFormFormDTO> OneAsync(long? memberId)
        {
            var dataInfo = await memberAppService.GetAsync(memberId);
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
        public async Task<ChildrenFormFormDTO> Save([FromBody] ChildrenFormFormDTO request)
        {
            if (request == null)
                throw new FriendlyException("参数错误", HttpStatusCode.BadRequest);

            var childInfo = await childRepository.GetAsync(p => p.MemberId == aspNetUser.ID);
            var child = request.Basic.Adapt<MemberChildren>();
            if (childInfo == null)
            {
                child.Id = YitIdHelper.NextId();
                child.MemberId = aspNetUser.ID;
                child.CreatedTime = DateTime.Now;
                child.EntityStatus = EntityStatus.New;
            }
            else
            {
                child.Id = childInfo.Id;
                child.MemberId = childInfo.MemberId;
                child.CreatedTime = childInfo.CreatedTime;
                child.EntityStatus = EntityStatus.Updated;
            }
            var childCondition = request.Condition.Adapt<MemberChildrenCondition>();
            if (childCondition != null)
            {
                var conditionInfo = await conditionRepository.GetAsync(p => p.MemberId == aspNetUser.ID);
                if (conditionInfo == null)
                {
                    childCondition.Id = YitIdHelper.NextId();
                    childCondition.MemberId = aspNetUser.ID;
                    childCondition.CreatedTime = DateTime.Now;
                    childCondition.EntityStatus = EntityStatus.New;
                }
                else
                {
                    childCondition.Id = conditionInfo.Id;
                    childCondition.MemberId = conditionInfo.MemberId;
                    childCondition.CreatedTime = conditionInfo.CreatedTime;
                    childCondition.EntityStatus = EntityStatus.Updated;
                }
            }
            var childImages = request.ImageList.Adapt<List<MemberChildrenImage>>();
            if (childImages?.Count > 0)
            {
                childImages.ForEach(item =>
                {
                    item.Id = YitIdHelper.NextId();
                    item.CreatedTime = DateTime.Now;
                    item.EntityStatus = EntityStatus.New;
                    item.MemberId = aspNetUser.ID;
                });
                child.DefaultImageUrl = childImages[0].Url;
            }
            var result = memberAppService.Save(child, childCondition, childImages);
            if (!result)
                throw new FriendlyException("保存失败", HttpStatusCode.InternalServerError);
            return new ChildrenFormFormDTO(child.Adapt<ChildrenDTO>(), childCondition != null ? childCondition.Adapt<ChildrenConditionDTO>() : null, childImages?.Count >= 0 ? childImages.Adapt<List<ChildrenImageDTO>>() : null);
        }
    }
}
