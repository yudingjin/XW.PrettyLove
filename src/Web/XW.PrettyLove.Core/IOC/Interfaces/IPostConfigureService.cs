namespace XW.PrettyLove.Core
{
    public interface IPostConfigureService
    {
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
