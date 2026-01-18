using FreeSql;
using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    public class AppCloud : FreeSqlCloud<DBEnum>
    {
        public AppCloud() : base(null) { }
        public AppCloud(string distributeKey) : base(distributeKey) { }
    }
}
