using LS.ERP.Domain;
using LS.ERP.Domain.System;
using Yitter.IdGenerator;

namespace LS.ERP.Infrastructure
{
    [ScopedDependency]
    public class MemberService : DomainService<Member>, IMemberService
    {
        public MemberService(IGenericRepository<Member, long> repository) : base(repository)
        {

        }

        public Task<Member> GetByOpenIdAsync(string openId)
        {
            return repository.Where(x => x.OpenId == openId).ToOneAsync();
        }

        public async Task<Member> SaveOrUpdateAsync(Member member)
        {
            var existingMember = await GetByOpenIdAsync(member.OpenId);
            if (existingMember == null)
            {
                member.Id = YitIdHelper.NextId();
                // 新用户，创建记录
                return await CreateAsync(member);
            }
            return existingMember;
        }
    }
}
