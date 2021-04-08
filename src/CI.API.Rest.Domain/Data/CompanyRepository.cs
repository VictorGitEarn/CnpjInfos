using CI.API.Rest.Domain.Business;
using CI.API.Rest.Domain.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Data
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companies;
        private readonly ConfigHelper _config;

        public CompanyRepository(ConfigHelper config)
        {
            _config = config;
            var client = new MongoClient(_config.MongoDBConnection);
            var database = client.GetDatabase(_config.DataBase);

            _companies = database.GetCollection<Company>(_config.CompaniesCollection);
        }

        public Company GetBySocialSecurity(string socialSecurity) => _companies.Find(c => c.SocialSecurity == socialSecurity).FirstOrDefault();


    }
}
