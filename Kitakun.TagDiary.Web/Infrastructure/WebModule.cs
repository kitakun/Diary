namespace Kitakun.TagDiary.Web.Infrastructure
{
    using Autofac;

    using Microsoft.AspNetCore.Http;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Services;
    using Kitakun.ExternalLogin.Abstraction;

    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<WebContext>().As<IWebContext>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

            builder.RegisterType<MasterPasswordProtectedFilter>().AsSelf().InstancePerLifetimeScope();
            
        }
    }
}
