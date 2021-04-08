using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Config
{
    public class ConfigHelper
    {
        private readonly IConfiguration _config;

        public ConfigHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", true, true)
                .AddJsonFile($"appsettings.Production.json", true, true)
                .Build();
        }

        public string MongoDBConnection => $"{_config.GetSection("ConnectionString").Value}";
        public string DataBase => $"{_config.GetSection("DataBase").Value}";
        public string CompaniesCollection => $"{_config.GetSection("CompaniesCollection").Value}";
    }
}
