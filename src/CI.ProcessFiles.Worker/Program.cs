using CI.ProcessFiles.Domain.Config;
using CI.ProcessFiles.Domain.Data;
using CI.ProcessFiles.Domain.Services.ExtractFiles;
using CI.ProcessFiles.Domain.Services.ReadFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CI.ProcessFiles.Worker
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

                    services.AddSingleton<ConfigHelper>();
                    
                    services.AddScoped<ICompanyRepository, CompanyRepository>();

                    services.AddScoped<IExtractFileService, ExtractFileService>();
                    services.AddScoped<IReadFileService, ReadFileService>();
                });
    }
}
