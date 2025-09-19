using Microsoft.AspNetCore.Builder;

namespace XW.PrettyLove.Core
{
    /// <summary>
    /// 应用上下文
    /// </summary>
    public class ApplicationInitializationContext
    {
        public WebApplicationBuilder Builder { get; set; }

        public ApplicationInitializationContext(WebApplicationBuilder builder)
        {
            this.Builder = builder;
        }
    }
}
