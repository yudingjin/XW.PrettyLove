namespace LS.ERP.Infrastructure
{
    public interface IPostConfigureService
    {
        void PostConfigureServices(ServiceConfigurationContext context);
    }
}
