using XW.PrettyLove.Domain;
using XW.PrettyLove.Domain.Shared;

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
        /// 获取子女详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Tuple<MemberChildren, MemberChildrenCondition, List<MemberChildrenImage>>> GetAsync(long? id);

        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="child"></param>
        /// <param name="condition"></param>
        /// <param name="imageList"></param>
        /// <returns></returns>
        bool Save(MemberChildren child, MemberChildrenCondition condition, List<MemberChildrenImage> imageList);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <param name="minHeight"></param>
        /// <param name="maxHeight"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        Task<IPagedList<MemberChildren>> GetPagedListAsync(Gender? gender, int? minAge, int? maxAge, int? minHeight, int? maxHeight, int PageIndex = 1, int PageSize = 20);
    }
}
