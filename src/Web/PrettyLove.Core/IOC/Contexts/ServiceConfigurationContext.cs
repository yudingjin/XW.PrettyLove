using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrettyLove.Core
{
    /// <summary>
    /// 服务上下文
    /// </summary>
    public class ServiceConfigurationContext
    {
        public IServiceCollection Services { get; }
        public IConfiguration Configuration { get; }
        public IDictionary<string, object> Items { get; }
        public object this[string key]
        {
            get => Items.GetOrDefault(key);
            set => Items[key] = value;
        }

        public ServiceConfigurationContext(IServiceCollection services, IConfiguration configuration)
        {
            this.Services = services;
            this.Configuration = configuration;
            this.Items = new Dictionary<string, object>();
        }
    }
}
