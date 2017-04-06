namespace Security.Config
{
    public static class InjectionConfig
    {
        public static void ConfigureInjections(SimpleInjector.Container container)
        {
            container.Register<Security.Handlers.APIKeyHandler>(SimpleInjector.Lifestyle.Singleton);
            container.Register<Security.Handlers.AuthHandler>(SimpleInjector.Lifestyle.Singleton);
        }
    }
}
