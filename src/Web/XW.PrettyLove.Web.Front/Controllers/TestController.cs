using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using XW.PrettyLove.Domain;

namespace XW.PrettyLove.Web.Front.Controllers
{
#if DEBUG

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IGenericRepository<Member> repository;
        private readonly IGenericRepository<MemberChildren> memberRepository;
        private readonly IGenericRepository<MemberChildrenCondition> memberConditionRepository;
        private readonly IGenericRepository<MemberChildrenImage> memberChildrenImageRepository;

        public TestController
        (
            IGenericRepository<Member> repository,
            IGenericRepository<MemberChildren> memberRepository,
            IGenericRepository<MemberChildrenCondition> memberConditionRepository,
            IGenericRepository<MemberChildrenImage> memberChildrenImageRepository
        )
        {
            this.repository = repository;
            this.memberRepository = memberRepository;
            this.memberConditionRepository = memberConditionRepository;
            this.memberChildrenImageRepository = memberChildrenImageRepository;
        }

        [HttpGet]
        public async Task<dynamic> Index()
        {
            var d = await repository.GetByKeyAsync(1);
            var memberInfo = await memberRepository.GetByKeyAsync(1);
            var memberConditionInfo = await memberConditionRepository.GetByKeyAsync(1);
            var memberImageInfo = await memberChildrenImageRepository.GetByKeyAsync(1);
            return memberInfo;
        }
    }

#endif
}
