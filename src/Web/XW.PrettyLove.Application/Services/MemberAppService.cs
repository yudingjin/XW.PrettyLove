using Microsoft.Extensions.Configuration;
using SKIT.FlurlHttpClient.Wechat.Api;
using SKIT.FlurlHttpClient.Wechat.Api.Models;
using XW.PrettyLove.Core;
using XW.PrettyLove.Domain;
using Yitter.IdGenerator;

namespace XW.PrettyLove.Application
{
    [ScopedDependency]
    public class MemberAppService : AppService<Member>, IMemberAppService
    {
        private readonly IConfiguration configuration;

        public MemberAppService(IGenericRepository<Member, long> repository, IConfiguration configuration) : base(repository)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<Member> Login(WechatLoginRequestDTO requestDto)
        {
            var options = new WechatApiClientOptions
            {
                AppId = configuration["Wechat:AppId"],
                AppSecret = configuration["Wechat:AppSecret"],
            };
            using (var client = WechatApiClientBuilder.Create(options).Build())
            {
                var response = await client.ExecuteSnsJsCode2SessionAsync(new SnsJsCode2SessionRequest { JsCode = requestDto.Code });
                if (response.IsSuccessful() != true)
                    throw new Exception(response.ErrorMessage);
                var memberInfo = await repository.GetAsync(p => p.OpenId == response.OpenId);
                if (memberInfo == null)
                {
                    memberInfo = new Member();
                    memberInfo.Id = YitIdHelper.NextId();
                    memberInfo.NickName = requestDto.NickName;
                    memberInfo.EntityStatus = Domain.Shared.EntityStatus.New;
                    memberInfo.OpenId = response.OpenId;
                    memberInfo.Gender = requestDto.Gender ?? Domain.Shared.Gender.None;
                    memberInfo.AvatarUrl = requestDto.AvatarUrl;
                    memberInfo.Country = requestDto.Country ?? string.Empty;
                    memberInfo.Province = requestDto.Province ?? string.Empty;
                    memberInfo.City = requestDto.City ?? string.Empty;
                    memberInfo.CreatedTime = DateTime.Now;
                    await repository.InsertAsync(memberInfo);
                }
                memberInfo.SessionKey = response.SessionKey;
                return memberInfo;
            }
        }
    }
}
