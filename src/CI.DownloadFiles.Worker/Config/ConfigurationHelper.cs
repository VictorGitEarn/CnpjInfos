using Microsoft.Extensions.Configuration;

namespace CI.DownloadFiles.Worker.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string Provider => $"{_config.GetSection("Provider").Value}";
        public string WebDrivers => $"{_config.GetSection("WebDrivers").Value}";
        public string ReceitaFederalUrl => $"{_config.GetSection("ReceitaFederalUrl").Value}";
        public string ShouldDownloadFromDemo => $"{_config.GetSection("ShouldDownloadFromDemo").Value}";
        public string DemoUrl => $"{_config.GetSection("DemoUrl").Value}";
        public string DemoElementToDownload => $"{_config.GetSection("DemoElementToDownload").Value}";
        public string DemoConfirmElementToDownload => $"{_config.GetSection("DemoConfirmElementToDownload").Value}";
        public string DownloadDir => $"{_config.GetSection("DownloadDir").Value}";
    }
}
