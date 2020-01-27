namespace Kitakun.TagDiary.Web.Infrastructure
{
    using Autofac;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc;

    using Kitakun.TagDiary.Core.Services;
    using Kitakun.TagDiary.Web.Services;
    using Kitakun.ExternalLogin.Abstraction;
    using Kitakun.TagDiary.Web.Infrastructure.Services;
    using Kitakun.TagDiary.Web.Infrastructure.Filters;

    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.Register(c => c.Resolve<IUrlHelperFactory>().GetUrlHelper(c.Resolve<IActionContextAccessor>().ActionContext))
                .As<IUrlHelper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DiaryUrlService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<WebContext>().As<IWebContext>().InstancePerLifetimeScope();
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

            // Filters
            builder.RegisterType<MasterPasswordProtectedFilter>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<SetTitleSpaceOwnerNameFilter>().AsSelf().InstancePerLifetimeScope();

        }
    }
}
