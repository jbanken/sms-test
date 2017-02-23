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
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            Service.Config.InjectionConfig.ConfigureInjections(container);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}