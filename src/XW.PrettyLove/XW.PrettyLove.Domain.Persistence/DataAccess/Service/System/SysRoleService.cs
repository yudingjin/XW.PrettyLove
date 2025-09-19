using LS.ERP.Domain;
using LS.ERP.Domain.Shared;
using LS.ERP.Domain.System;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 角色领域服务
    /// </summary>
    [ScopedDependency]
    public class SysRoleService : DomainService<SysRole>, ISysRoleService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public SysRoleService(IGenericRepository<SysRole, long> repository) : base(repository)
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
        public async Task<IPagedList<SysRole>> GetPagedAsync(string name, CommonStatus? status, int pageIndex = 1, int pageSize = 20)
        {
            var query = Select.WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name))
                              .WhereIf(status != null, x => x.Status == status);
            var count = query.Count();
            var list = await query.Page(pageIndex, pageSize).ToListAsync();
            return new PagedList<SysRole>(pageIndex, pageSize, list, count);
        }
    }
}
