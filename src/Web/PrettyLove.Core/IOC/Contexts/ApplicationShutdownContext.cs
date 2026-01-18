namespace PrettyLove.Core
{
    public class ApplicationShutdownContext
    {
        public IServiceProvider ServiceProvider { get; }
        public ApplicationShutdownContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
