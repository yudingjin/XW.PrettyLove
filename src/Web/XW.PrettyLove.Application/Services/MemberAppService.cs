using Microsoft.Extensions.Configuration;
using SKIT.FlurlHttpClient.Wechat.Api;
using SKIT.FlurlHttpClient.Wechat.Api.Models;
using System.Net;
using XW.PrettyLove.Core;
using XW.PrettyLove.Domain;
using Yitter.IdGenerator;

namespace XW.PrettyLove.Application
{
    [ScopedDependency]
    public class MemberAppService : AppService<Member>, IMemberAppService
    {
        private readonly IGenericRepository<MemberChildren, long> childRepository;
        private readonly IGenericRepository<MemberChildrenCondition, long> conditionRepository;
        private readonly IGenericRepository<MemberChildrenImage, long> imageRepository;
        private readonly IConfiguration configuration;

        public MemberAppService
        (
            IGenericRepository<Member, long> repository,
            IGenericRepository<MemberChildren, long> childRepository,
            IGenericRepository<MemberChildrenCondition, long> conditionRepository,
            IGenericRepository<MemberChildrenImage, long> imageRepository,
            IConfiguration configuration
        ) : base(repository)
        {
            this.childRepository = childRepository;
            this.conditionRepository = conditionRepository;
            this.imageRepository = imageRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<Member> LoginAsync(WechatLoginRequestDTO requestDto)
        {
            var options = new WechatApiClientOptions
            {
                AppId = configuration["Wechat:AppId"],
                AppSecret = configuration["Wechat:AppSecret"],
            };
            var requestParameter = new SnsJsCode2SessionRequest
            {
                JsCode = requestDto.Code,
                GrantType = "authorization_code"
            };
            using var client = WechatApiClientBuilder.Create(options).Build();
            var response = await client.ExecuteSnsJsCode2SessionAsync(requestParameter);
            if (response.IsSuccessful() != true)
                throw new FriendlyException(response.ErrorMessage, HttpStatusCode.InternalServerError);
            var memberInfo = await repository.GetAsync(p => p.OpenId == response.OpenId);
            if (memberInfo == null)
            {
                memberInfo = new Member();
                memberInfo.Id = YitIdHelper.NextId();
                memberInfo.NickName = "微信用户_" + Guid.NewGuid().ToString();
                memberInfo.EntityStatus = Domain.Shared.EntityStatus.New;
                memberInfo.OpenId = response.OpenId;
                memberInfo.Gender = requestDto.Gender ?? Domain.Shared.Gender.None;
                memberInfo.AvatarUrl = requestDto.AvatarUrl ?? string.Empty;
                memberInfo.Country = requestDto.Country ?? string.Empty;
                memberInfo.Province = requestDto.Province ?? string.Empty;
                memberInfo.City = requestDto.City ?? string.Empty;
                memberInfo.CreatedTime = DateTime.Now;
                memberInfo.SessionKey = response.SessionKey;
                await repository.InsertAsync(memberInfo);
            }
            memberInfo.SessionKey = response.SessionKey;
            return memberInfo;
        }

        /// <summary>
        /// 更新手机号码
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public async Task<int> UpdatePhoneAsync(Member member)
        {
            return await repository.ModifyAsync(member, p => p.Phone);
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<Tuple<MemberChildren, MemberChildrenCondition, List<MemberChildrenImage>>> GetChildrenAsync(long? memberId)
        {
            var childInfo = await childRepository.GetAsync(p => p.MemberId == memberId);
            if (childInfo == null)
                throw new FriendlyException("数据不存在", HttpStatusCode.NotFound);
            var condtionInfo = await conditionRepository.GetAsync(p => p.MemberId == memberId);
            var imageList = await imageRepository.GetListAsync(p => p.MemberId == memberId);
            return new Tuple<MemberChildren, MemberChildrenCondition, List<MemberChildrenImage>>(childInfo, condtionInfo, imageList);
        }

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="child"></param>
        /// <param name="condition"></param>
        /// <param name="imageList"></param>
        /// <returns></returns>
        public bool Save(MemberChildren child, MemberChildrenCondition condition, List<MemberChildrenImage> imageList)
        {
            if (child.EntityStatus == Domain.Shared.EntityStatus.New)
                childRepository.Insert(child);
            else
                childRepository.Modify(child);
            if (condition != null)
            {
                if (condition.EntityStatus == Domain.Shared.EntityStatus.New)
                    conditionRepository.Insert(condition);
                else
                    conditionRepository.Modify(condition);
            }
            if (imageList?.Count > 0)
            {
                var memberId = imageList[0].MemberId;
                imageRepository.Delete(p => p.MemberId == memberId);
                imageRepository.Insert(imageList);
            }
            return true;
        }
    }
}
