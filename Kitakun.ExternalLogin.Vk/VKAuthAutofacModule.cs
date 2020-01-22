namespace Kitakun.ExternalLogin.Vk
{
    using Autofac;

    using Kitakun.ExternalLogin.Abstraction;

    public class VKAuthAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VkAuth>().As<IAuthProvider>().InstancePerLifetimeScope();
        }
    }
}
