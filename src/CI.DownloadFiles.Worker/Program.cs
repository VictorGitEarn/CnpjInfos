using CI.DownloadFiles.Worker.Config;
using CI.DownloadFiles.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CI.DownloadFiles.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services.AddSingleton<ConfigurationHelper>();
                    services.AddScoped<IDownloadFileService, DownloadFileService>();
                });
    }
}
