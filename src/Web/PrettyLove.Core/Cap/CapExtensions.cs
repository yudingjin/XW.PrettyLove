using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrettyLove.Core
{
    public static class CapExtensions
    {
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration)
        {
            //添加数据库上下文
            services.AddDbContext<CapDbContext>(options =>
            {
                options.UseMySql(configuration["DbConfig:WriteConfig"], new MySqlServerVersion("5.7"));
            });

            //CAP配置
            services.AddCap(x =>
            {
                //1.1 使用RabbitMQ存储消息
                x.UseRabbitMQ(rb =>
                {
                    var config = configuration.GetSection("RabbitMQConfig").Get<RabbitMQOptions>();
                    rb.HostName = config.HostName;
                    rb.Port = config.Port;
                    rb.UserName = config.UserName;
                    rb.Password = config.Password;
                    rb.VirtualHost = config.VirtualHost;
                });

                //1.2 存储消息  使用数据库持久性 是为了防止rabbitmq 失败  数据丢失  
                x.UseMySql(sql =>
                {
                    sql.ConnectionString = configuration["DbConfig:WriteConfig"];
                });

                //1.3 消息重试
                x.FailedRetryInterval = 15000;    //时间间隔
                x.FailedRetryCount = 5;        //失败次数  如果超过这个数字 只能进行人工处理   Dashboard
                x.SucceedMessageExpiredAfter = 24 * 3600 * 1;
                x.FailedMessageExpiredAfter = 24 * 3600 * 7;

                //1.4 仪表盘  人工重试
                x.UseDashboard();
            });
            return services;
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="dashboardPath"></param>
        /// <returns></returns>
        public static IServiceCollection AddCap(this IServiceCollection services, IConfiguration configuration, string dashboardPath)
        {
            //添加数据库上下文
            services.AddDbContext<CapDbContext>(options =>
            {
                options.UseMySql(configuration["DbConfig:WriteConfig"], new MySqlServerVersion("5.7"));
            });

            //CAP配置
            services.AddCap(x =>
            {
                //1.1 使用RabbitMQ存储消息
                x.UseRabbitMQ(rb =>
                {
                    var config = configuration.GetSection("RabbitMQConfig").Get<RabbitMQOptions>();
                    rb.HostName = config.HostName;
                    rb.Port = config.Port;
                    rb.UserName = config.UserName;
                    rb.Password = config.Password;
                    rb.VirtualHost = config.VirtualHost;
                });

                //1.2 存储消息  使用数据库持久性 是为了防止rabbitmq 失败  数据丢失  
                x.UseMySql(sql =>
                {
                    sql.ConnectionString = configuration["DbConfig:WriteConfig"];
                });

                //1.3 消息重试
                x.FailedRetryInterval = 1000;       //时间间隔
                x.FailedRetryCount = 5;             //失败次数  如果超过这个数字 只能进行人工处理   Dashboard
                x.SucceedMessageExpiredAfter = 24 * 3600 * 1;
                x.FailedMessageExpiredAfter = 24 * 3600 * 7;

                //1.4 仪表盘  人工重试
                x.UseDashboard(options =>
                {
                    options.PathMatch = dashboardPath ?? "/cap";
                });
            });
            return services;
        }
    }
}
