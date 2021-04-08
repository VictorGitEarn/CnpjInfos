using CI.ProcessFiles.Domain.Business;
using CI.ProcessFiles.Domain.Config;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CI.ProcessFiles.Domain.Data
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

        public Company GetCompanyBySocialSecurity(string socialSecurity) => _companies.Find(c => c.SocialSecurity == socialSecurity).FirstOrDefault();

        public void Save(Company company) => _companies.InsertOne(company);

        public void Delete(Company company) =>_companies.DeleteOne(c => c.SocialSecurity == company.SocialSecurity);
    }
}
