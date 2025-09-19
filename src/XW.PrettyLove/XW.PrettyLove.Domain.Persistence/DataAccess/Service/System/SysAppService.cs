using LS.ERP.Domain;
using LS.ERP.Domain.Shared;
using LS.ERP.Domain.System;

namespace LS.ERP.Infrastructure
{
    [ScopedDependency]
    public class SysAppService : DomainService<SysApp>, ISysAppService
    {
        public SysAppService(IGenericRepository<SysApp, long> repository): base(repository)
        {

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="appType"></param>
        /// <param name="status"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IPagedList<SysApp>> GetPagedAsync(AppType? appType,CommonStatus? status, string code, string name, int pageIndex = 1, int pageSize = 20)
        {
            var query = Select.WhereIf(appType.HasValue, x => x.AppType == appType)
                              .WhereIf(status.HasValue, x => x.Status == status)
                              .WhereIf(!string.IsNullOrWhiteSpace(code), x => x.Code.Contains(code))
                              .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name));
            var count = query.Count();
            var list = await query.Page(pageIndex, pageSize).ToListAsync();
            return new PagedList<SysApp>(pageIndex, pageSize, list, count);
        }
    }
}
