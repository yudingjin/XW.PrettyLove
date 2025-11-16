using XW.PrettyLove.Domain;

namespace XW.PrettyLove.Application
{
    public interface IMemberAppService : IAppService<Member>
    {
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        Task<Member> Login(WechatLoginRequestDTO requestDto);
    }
}
