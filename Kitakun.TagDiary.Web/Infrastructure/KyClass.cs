namespace Kitakun.TagDiary.Web.Infrastructure
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Configuration;

    public class KyClass
    {
        public static DirectoryInfo GetKyRingDirectoryInfo(IConfiguration configuration)
        {
            string applicationBasePath = AppContext.BaseDirectory;
            DirectoryInfo directoryInof = new DirectoryInfo(applicationBasePath);
            string keyRingPath = configuration.GetSection("AppKeys").GetValue<string>("keyRingPath");
            do
            {
                directoryInof = directoryInof.Parent;

                DirectoryInfo keyRingDirectoryInfo = new DirectoryInfo($"{directoryInof.FullName}{keyRingPath}");
                if (keyRingDirectoryInfo.Exists)
                {
                    return keyRingDirectoryInfo;
                }
                else
                {
                    Console.WriteLine($"Key searcher: path '{keyRingDirectoryInfo.FullName}' is not exists");
                }
            }
            while (directoryInof.Parent != null);
            throw new Exception($"key ring path not found");
        }
    }
}
