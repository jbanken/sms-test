using SimpleInjector;

namespace Logger.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(Container container)
        {
            container.Register<Logger.Interfaces.ILogService, LogService>(Lifestyle.Singleton);

            DataProvider.Config.InjectionConfig.ConfigureInjections(container);
        }
    }
}
