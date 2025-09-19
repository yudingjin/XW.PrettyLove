using LS.ERP.Domain;
using LS.ERP.Domain.Shared;
using LS.ERP.Domain.System;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 租户领域服务实现
    /// </summary>
    [ScopedDependency]
    public class SysTenantService : DomainService<SysTenant>, ISysTenantService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public SysTenantService(IGenericRepository<SysTenant, long> repository) : base(repository)
        {

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IPagedList<SysTenant>> GetPagedAsync(string name, CommonStatus? status, int pageIndex = 1, int pageSize = 20)
        {
            var query = Select.WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name))
                              .WhereIf(status != null, x => x.Status == status);
            var count = query.Count();
            var list = await query.Page(pageIndex, pageSize).ToListAsync();
            return new PagedList<SysTenant>(pageIndex, pageSize, list, count);
        }
    }
}
