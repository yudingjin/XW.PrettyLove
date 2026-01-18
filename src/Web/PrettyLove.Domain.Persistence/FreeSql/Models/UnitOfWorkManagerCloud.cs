using FreeSql;
using System.Data;
using PrettyLove.Domain.Shared;

namespace PrettyLove.Core
{
    public class UnitOfWorkManagerCloud
    {
        readonly Dictionary<DBEnum, UnitOfWorkManager> managers = new Dictionary<DBEnum, UnitOfWorkManager>();
        readonly AppCloud cloud;
        public UnitOfWorkManagerCloud(AppCloud cloud)
        {
            this.cloud = cloud;
        }

        public UnitOfWorkManager GetUnitOfWorkManager(DBEnum db)
        {
            if (managers.TryGetValue(db, out var uowm) == false)
                managers.Add(db, uowm = new UnitOfWorkManager(cloud.Use(db)));
            return uowm;
        }

        public void Dispose()
        {
            foreach (var uowm in managers.Values) uowm.Dispose();
            managers.Clear();
        }

        public IUnitOfWork Begin(DBEnum db, Propagation propagation = Propagation.Required, IsolationLevel? isolationLevel = null)
        {
            return GetUnitOfWorkManager(db).Begin(propagation, isolationLevel);
        }
    }
}
