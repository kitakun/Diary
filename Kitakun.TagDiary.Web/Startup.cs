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
    using Microsoft.AspNetCore.DataProtection;

    using Kitakun.TagDiary.Web.Infrastructure;

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

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

#if RELEASE
            services.AddDataProtection()
                .PersistKeysToFileSystem(KyClass.GetKyRingDirectoryInfo(Configuration))
                .SetApplicationName("SharedDiaryKey");
#endif

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.LoginPath = "/Auth/LoginPage";
                    options.Cookie.Name = ".Diary.SharedAuth";
                    options.Cookie.Domain = Configuration.GetSection("All").GetValue<string>("DomainName");
                });
        }

        public void ConfigureContainer(ContainerBuilder builder) => builder.Configurate();

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "auth",
                    template: "externalAuth/{providerName}",
                    defaults: new { controller = "Auth", action = "ExternalAuth" });
            });
        }
    }
}
