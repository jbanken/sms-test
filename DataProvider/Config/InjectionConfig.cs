using SimpleInjector;
namespace DataProvider.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(Container container)
        {
            container.Register<Interfaces.ISMSDataProvider, SMSDataProvider>(Lifestyle.Singleton);
        }
    }
}
