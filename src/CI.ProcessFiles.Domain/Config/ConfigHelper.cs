using Microsoft.Extensions.Configuration;

namespace CI.ProcessFiles.Domain.Config
{
    public class ConfigHelper
    {
        private readonly IConfiguration _config;

        public ConfigHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string Provider => $"{_config.GetSection("Provider").Value}";
        public string DownloadDir => $"{_config.GetSection("DownloadDir").Value}";
        public string MongoDBConnection => $"{_config.GetSection("ConnectionString").Value}";
        public string DataBase => $"{_config.GetSection("DataBase").Value}";
        public string CompaniesCollection => $"{_config.GetSection("CompaniesCollection").Value}";
        public string ExtractAgain => $"{_config.GetSection("ExtractAgain").Value}";
    }
}
