using LS.ERP.Domain;
using LS.ERP.Domain.Shared;
using LS.ERP.Domain.System;

namespace LS.ERP.Infrastructure
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    [ScopedDependency]
    public class SysUserService : DomainService<SysUser>, ISysUserService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        public SysUserService(IGenericRepository<SysUser, long> repository) : base(repository)
        {

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="userType"></param>
        /// <param name="status"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public async Task<IPagedList<SysUser>> GetPagedAsync(long? departmentId, string account, string name, string phone, UserType? userType, CommonStatus? status, int pageIndex = 1, int pageSize = 20)
        {
            var query = Select.WhereIf(departmentId != null, x => x.DepartmentId == departmentId)
                              .WhereIf(!string.IsNullOrWhiteSpace(account), x => x.Account == account)
                              .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name))
                              .WhereIf(!string.IsNullOrWhiteSpace(phone), x => x.Phone == phone)
                              .WhereIf(userType != null, x => x.UserType == userType)
                              .WhereIf(status != null, x => x.Status == status);
            var count = query.Count();
            var list = await query.Page(pageIndex, pageSize).ToListAsync();
            return new PagedList<SysUser>(pageIndex, pageSize, list, count);
        }
    }
}
