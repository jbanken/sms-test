using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSProvider.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(Container container)
        {
            container.Register<Interfaces.ITwilioSMSProvider, TwilioSMSProvider>(Lifestyle.Singleton);
        }
    }
}
