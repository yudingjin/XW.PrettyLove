using FreeSql;
using LS.ERP.Domain.Shared;

namespace LS.ERP.Infrastructure
{
    public class AppCloud : FreeSqlCloud<DBEnum>
    {
        public AppCloud() : base(null) { }
        public AppCloud(string distributeKey) : base(distributeKey) { }
    }
}
