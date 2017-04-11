using API.Handlers;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace API.App_Start
{
    public static class InjectionConfig
    {
        public static void Configure()
        {
            var container = new Container();
            container.Options.AllowOverridingRegistrations = true;
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            Logger.Config.InjectionConfig.ConfigureInjections(container);
            Service.Config.InjectionConfig.ConfigureInjections(container);
            
            container.Register<Handlers.LoggingHandler>(Lifestyle.Singleton);
            container.Register<API.Handlers.APIKeyHandler>(SimpleInjector.Lifestyle.Singleton);
            container.Register<API.Handlers.AuthHandler>(SimpleInjector.Lifestyle.Singleton);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<API.Handlers.AuthHandler>(container));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<API.Handlers.APIKeyHandler>(container));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new DelegatingHandlerProxy<Handlers.LoggingHandler>(container));
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}