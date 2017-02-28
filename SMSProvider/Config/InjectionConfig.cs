using SimpleInjector;

namespace SMSProvider.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(Container container)
        {
            container.Register<Interfaces.ITwilioSMSProvider, TwilioSMSProvider>(Lifestyle.Singleton);

            Logger.Config.InjectionConfig.ConfigureInjections(container);
        }
    }
}
