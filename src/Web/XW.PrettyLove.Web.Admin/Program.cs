using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using XW.PrettyLove.Core;

namespace XW.PrettyLove.Web.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory())
                        .ConfigureAppConfiguration((hostContext, config) => hostContext.Configuration.ConfigureApplication());
            builder.ConfigureApplication();
            ModuleLoader.Run(builder, typeof(HostModule));
        }
    }
}
