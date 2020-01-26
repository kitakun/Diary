//#define USE_HTTPS

namespace Kitakun.TagDiary.Web
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using Autofac.Extensions.DependencyInjection;

    using Kitakun.TagDiary.Persistance;

    public class Program
    {
        public const int HttpPort = 5010;
        public const int HttpsPort = 5011;

        public static void Main(string[] args)
        {
            var buildedApp = CreateWebHostBuilder(args).Build();
            // MigrateDbOnStartup(buildedApp);
            buildedApp.Run();
        }

        private static void MigrateDbOnStartup(IWebHost buildedApp)
        {
            using (var scope = buildedApp.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    var config = services.GetRequiredService<IConfiguration>();
                    var dbConfigString = config.GetValue<string>("DBInfo:ConnectionString");
                    logger.LogInformation($"Db config: {dbConfigString}");

                    var context = services.GetRequiredService<DiaryDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occu rred seeding the DB.");
                    Console.ReadKey();
                    return;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
#if RELEASE
                .UseKestrel(c =>
                {
                    c.ListenAnyIP(HttpPort);
#if USE_HTTPS
                    c.ListenAnyIP(HttpsPort, cc =>
                    {
                        cc.UseHttps("qwe.pfx", "qwe");
                    });
#endif
                })
#endif
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                
#if DEBUG
                .UseUrls(urls: new string[] { $"http://*:{HttpPort}", $"https://*:{HttpsPort}" })
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
