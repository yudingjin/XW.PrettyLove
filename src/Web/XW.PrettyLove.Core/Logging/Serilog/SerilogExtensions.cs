using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// Serilog扩展方法
    /// </summary>
    public static class SerilogExtensions
    {
        /// <summary>
        /// 默认输出模板
        /// </summary>
        private static readonly string defaultConsoleOutputTemplate = "【时间】{Timestamp:yyyy-MM-dd HH:mm:ss,fff}{NewLine}【等级】[{Level}]{NewLine}【消息】[{SourceContext}] {Message:lj}{NewLine}{Exception}{NewLine}";

        /// <summary>
        /// Serilog配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Host.UseSerilog((context, logger) =>
            {
#if DEBUG
                logger.MinimumLevel.Information();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information);
#else
                logger.MinimumLevel.Error();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error);
#endif                
                logger.Enrich.With(new ThreadIdEnricher());
                logger.Enrich.FromLogContext();

#if DEBUG
                logger.WriteTo.Console(LogEventLevel.Information, defaultConsoleOutputTemplate);
#else
                logger.WriteTo.Console(LogEventLevel.Error, defaultConsoleOutputTemplate);
#endif
                //logger.WriteTo.Exceptionless(configuration["ExceptionLess:ApiKey"], configuration["ExceptionLess:ServerUrl"]);
            });
            return builder;
        }

        /// <summary>
        /// Serilog配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="apiKey"></param
        /// <param name="serverUrl"></param>
        public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder, string apiKey, string serverUrl)
        {
            builder.Host.UseSerilog((context, logger) =>
            {
#if DEBUG
                logger.MinimumLevel.Information();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information);
#else
                logger.MinimumLevel.Error();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error);
#endif                
                logger.Enrich.With(new ThreadIdEnricher());
                logger.Enrich.FromLogContext();

#if DEBUG
                logger.WriteTo.Console(LogEventLevel.Information, defaultConsoleOutputTemplate);
#else
                logger.WriteTo.Console(LogEventLevel.Error, defaultConsoleOutputTemplate);
#endif
                //logger.WriteTo.Exceptionless(apiKey, serverUrl);
            });
            return builder;
        }

        /// <summary>
        /// Serilog配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static WebApplicationBuilder UseSerilogAsync(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Host.UseSerilog((context, logger) =>
            {
#if DEBUG
                logger.MinimumLevel.Information();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
#else
                logger.MinimumLevel.Error();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error);
#endif                
                logger.Enrich.With(new ThreadIdEnricher());
                logger.Enrich.FromLogContext();
#if DEBUG
                logger.WriteTo.Async(c => c.Console(LogEventLevel.Information, defaultConsoleOutputTemplate));
#else
                logger.WriteTo.Async(c => c.Console(LogEventLevel.Error, defaultConsoleOutputTemplate));
#endif
                //logger.WriteTo.Async(c => c.Exceptionless(configuration["ExceptionLess:ApiKey"], configuration["ExceptionLess:ServerUrl"]));
            });
            return builder;
        }

        /// <summary>
        /// Serilog配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static WebApplicationBuilder UseSerilogAsync(this WebApplicationBuilder builder, string apiKey, string serverUrl)
        {
            builder.Host.UseSerilog((context, logger) =>
            {
#if DEBUG
                logger.MinimumLevel.Information();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
#else
                logger.MinimumLevel.Error();
                logger.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
                logger.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error);
#endif                
                logger.Enrich.With(new ThreadIdEnricher());
                logger.Enrich.FromLogContext();
#if DEBUG
                logger.WriteTo.Async(c => c.Console(LogEventLevel.Information, defaultConsoleOutputTemplate));
#else
                logger.WriteTo.Async(c => c.Console(LogEventLevel.Error, defaultConsoleOutputTemplate));
#endif
                //logger.WriteTo.Async(c => c.Exceptionless(apiKey, serverUrl));
            });
            return builder;
        }
    }
}
