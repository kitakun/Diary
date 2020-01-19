namespace Kitakun.TagDiary.Web.Infrastructure
{
    using Autofac;

    using Kitakun.TagDiary.Persistance;
    using Kitakun.TagDiary.Services;

    public static class DependencyConfiguration
    {
        public static void Configurate(this ContainerBuilder container)
        {
            container.RegisterModule<PersistanceModule>();
            container.RegisterModule<ServicesModule>();
            container.RegisterModule<WebModule>();
        }
    }
}
