using CI.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Business
{
    public class Company : Entity, IAggregateRoot
    {
        public Company() : base() { }

        public Company(string socialSecurity, CompanyType companyType, string name, string tradeName, string socialCapital, Situation situation, DateTime situationDate, string cEP, List<Partner> partners)
        {
            SocialSecurity = socialSecurity;
            CompanyType = companyType;
            Name = name;
            TradeName = tradeName;
            SocialCapital = socialCapital;
            Situation = situation;
            SituationDate = situationDate;
            CEP = cEP;
            Partners = partners;
        }

        public string SocialSecurity { get; private set; }

        public CompanyType CompanyType { get; private set; }

        public string Name { get; private set; }

        public string TradeName { get; private set; }

        public string SocialCapital { get; private set; }

        public Situation Situation { get; private set; }

        public DateTime SituationDate { get; private set; }

        public string CEP { get; private set; }

        public List<Partner> Partners { get; private set; }
    }
}
