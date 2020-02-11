#define USE_HTTPS

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

        public static void Main(string[] args) =>
            CreateWebHostBuilder(args)
                .Build()
                // .MigrateDbOnStartup()
                .Run();

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
                        cc.UseHttps("TagDiary.pfx", Environment.GetEnvironmentVariable("HTTPS_PASSWORD"));
                    });
#endif
                })
#endif
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(services => services.AddAutofac())
#if DEBUG
                .UseUrls(urls: new string[] { $"http://*:{HttpPort}" })
#endif
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .UseStartup<Startup>();
    }

    internal static class ProgramExtensions
    {
        public static IWebHost MigrateDbOnStartup(this IWebHost buildedApp)
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
                    logger.LogError(ex, "An error occured while seeding DataBase.");
#if Debug
                    Console.ReadKey();
#endif
                    return buildedApp;
                }
            }
            return buildedApp;
        }
    }
}
