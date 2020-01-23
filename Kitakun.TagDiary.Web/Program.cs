namespace Kitakun.TagDiary.Web
{
#if RELEASE
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using Kitakun.TagDiary.Persistance;
#endif
    using System.IO;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;

    using Autofac.Extensions.DependencyInjection;

    public class Program
    {
        public const int HttpPort = 5010;
        public const int HttpsPort = 5011;

        public static void Main(string[] args)
        {
            var buildedApp = CreateWebHostBuilder(args).Build();

#if RELEASE
            using (var scope = buildedApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DiaryDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occu rred seeding the DB.");
                }
            }
#endif

            buildedApp.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
#if RELEASE
                .UseKestrel(c =>
                {
                    c.ListenAnyIP(HttpPort);
                    c.ListenAnyIP(HttpsPort, cc =>
                    {
                        cc.UseHttps("qwe.pfx", "qwe");
                    });
                })
#endif
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseWebRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls(urls: new string[] { $"http://*:{HttpPort}", $"https://*:{HttpsPort}" })
#if DEBUG
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
#endif
                .UseStartup<Startup>();
    }
}
