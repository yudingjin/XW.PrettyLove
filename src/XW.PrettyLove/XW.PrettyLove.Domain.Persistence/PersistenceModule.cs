using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace XW.PrettyLove.Core
{
    [DependsOn(typeof(CoreModule))]
    public class PersistenceModule : IModule
    {
        public void ConfigureServices(ServiceConfigurationContext context)
        {
            // 注册程序集服务
            context.Services.AddAssemblyServices(Assembly.GetExecutingAssembly());

            // FreeSql
            var list = context.Configuration.GetSection("DbConfigList").Get<List<DBItem>>();
            context.Services.AddFreeSqlCloud(list.ToArray());
        }
    }
}
