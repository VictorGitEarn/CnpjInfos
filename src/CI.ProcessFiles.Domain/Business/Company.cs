using CI.Core.DomainObjects;
using CI.ProcessFiles.Domain.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CI.ProcessFiles.Domain.Business
{
    public class Company : Entity, IAggregateRoot
    {
        public string SocialSecurity { get; private set; }

        public CompanyType CompanyType { get; private set; }

        public string Name { get; private set; }

        public string TradeName { get; private set; }

        public string SocialCapital { get; private set; }

        public Situation Situation { get; private set; }

        public DateTime SituationDate { get; private set; }

        public string CEP { get; private set; }

        public List<Partner> Partners { get; private set; }

        public Company() : base()
        {
            Partners = new List<Partner>();
        }

        public void AddPartner(Partner employee)
        {
            Partners.Add(employee);
        }

        public void AddCompanyType(string companyType)
        {
            if (!Enum.TryParse(companyType, true, out CompanyType type))
                throw new ApplicationException($"Não foi possível converter o identificador da empresa {companyType}");

            CompanyType = type;
        }

        public void AddSituationAndDate(string situationType, string situationDate)
        {
            if (!Enum.TryParse(situationType, true, out Situation situation))
                throw new ApplicationException($"Não foi possível converter o identificador da empresa {situationType}");

            Situation = situation;
            SituationDate = DateTime.ParseExact(situationDate, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public void AddNames(string name, string tradeName)
        {
            Name = name;
            TradeName = tradeName;
        }

        public void AddSocialInfos(string socialSecurity, string socialCapital)
        {
            SocialSecurity = socialSecurity;
            SocialCapital = socialCapital;
        }

        public void AddCEP(string cep)
        {
            CEP = cep;
        }
    }
}
