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
        Task<Member> LoginAsync(WechatLoginRequestDTO requestDto);

        /// <summary>
        /// 更新手机号码
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<int> UpdatePhoneAsync(Member member); 

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        Task<Tuple<MemberChildren, MemberChildrenCondition, List<MemberChildrenImage>>> GetChildrenAsync(long? memberId);

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="child"></param>
        /// <param name="condition"></param>
        /// <param name="imageList"></param>
        /// <returns></returns>
        bool Save(MemberChildren child, MemberChildrenCondition condition, List<MemberChildrenImage> imageList);
    }
}
