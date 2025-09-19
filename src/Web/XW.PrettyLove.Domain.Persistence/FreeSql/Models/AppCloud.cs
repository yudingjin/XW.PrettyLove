using FreeSql;
using XW.PrettyLove.Domain.Shared;

namespace XW.PrettyLove.Core
{
    public class AppCloud : FreeSqlCloud<DBEnum>
    {
        public AppCloud() : base(null) { }
        public AppCloud(string distributeKey) : base(distributeKey) { }
    }
}
