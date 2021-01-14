namespace Kitakun.TagDiary.Web
{
    using System;

    using Autofac;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Kitakun.TagDiary.Web.Infrastructure;
    using Kitakun.TagDiary.Web.Extensions;
    using Kitakun.TagDiary.Web.Controllers;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc(x =>
            {
                x.SslPort = Program.HttpsPort;
                // for razor
                x.EnableEndpointRouting = false;
            });

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.LoginPath = "/Auth/LoginPage";
                    options.Cookie.Name = ".Diary.SharedAuth";
                    options.Cookie.Domain = Configuration.GetSection("All").GetValue<string>("DomainName");
                });

#if RELEASE
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = Program.HttpsPort;
            });
#endif
        }

        public void ConfigureContainer(ContainerBuilder builder) => builder.Configurate();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCookiePolicy();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: DiaryWebConstants.RouteWithOwnerName,
                    template: $"{{{DiaryWebConstants.RouteByOwnerName}}}/{{controller=Home}}/{{action=Index}}/{{id?}}");

                //routes.MapRoute(
                //    name: "inDiaryRoot",
                //    template: $"{{{DiaryWebConstants.RouteByOwnerName}}}/{{controller={ControllerExtensions.GetControllerName<SpaceOwnerController>()}}}/{{action={nameof(SpaceOwnerController.Index)}}}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: DiaryWebConstants.RouteByOwnerName,
                    template: $"{{{DiaryWebConstants.RouteByOwnerName}}}",
                    defaults: new { controller = "SpaceOwner", action = "Index" });

                routes.MapRoute(
                    name: "auth",
                    template: "externalAuth/{providerName}",
                    defaults: new { controller = "Auth", action = "ExternalAuth" });
            });
        }
    }
}
