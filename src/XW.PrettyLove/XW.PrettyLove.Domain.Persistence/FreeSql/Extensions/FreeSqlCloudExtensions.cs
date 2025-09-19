using FreeSql;
using LS.ERP.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LS.ERP.Infrastructure
{
    public static class FreeSqlCloudExtensions
    {
        /// <summary>
        /// FreeSqlCloud注册 多数据库支持
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dbItemArray"></param>
        /// <returns></returns>
        public static IServiceCollection AddFreeSqlCloud(this IServiceCollection services, params DBItem[] dbItemArray)
        {
            AppCloud freeSqlCloud = new AppCloud();
            // 注册数据库
            foreach (var item in dbItemArray)
            {
                freeSqlCloud.Register(item.DBEnum, () =>
                {
                    FreeSqlBuilder builder = new FreeSqlBuilder();
                    builder.UseConnectionString(item.DataType, item.MasterConnection);
                    if (item.SlaveConnectionList?.Length > 0)
                        builder.UseSlave(item.SlaveConnectionList);
#if DEBUG
                    builder.UseAutoSyncStructure(true);  //自动迁移
                    builder.UseNoneCommandParameter(true);
                    builder.UseMonitorCommand(cmd =>
                    {
                        Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine($"Sql：{cmd.CommandText}");
                        Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                    });
#endif
                    builder.UseExitAutoDisposePool(true);
                    var freeSql = builder.Build().AddGobalFilter().AddConfigEntity().AddConfigEntityProperty().AddCurdAfter();
                    return freeSql;
                });
            }

            //注册FreeSql
            services.AddSingleton<IFreeSql>(freeSqlCloud);
            services.AddSingleton(freeSqlCloud);
            //注册仓储
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IDomainService<,>), typeof(DomainService<,>));
            services.AddScoped(typeof(IDomainService<>), typeof(DomainService<>));
            services.AddScoped<UnitOfWorkManagerCloud>();
            return services;
        }
    }
}
