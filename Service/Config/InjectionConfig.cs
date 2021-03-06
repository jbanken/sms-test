﻿using SimpleInjector;

namespace Service.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(Container container)
        {
            container.Register<Interfaces.ISMSService, SMSService>(Lifestyle.Singleton);
            container.Register<Interfaces.ITwilioService, TwilioService>(Lifestyle.Singleton);

            DataProvider.Config.InjectionConfig.ConfigureInjections(container);
            SMSProvider.Config.InjectionConfig.ConfigureInjections(container);
            Logger.Config.InjectionConfig.ConfigureInjections(container);
        }
    }
}
