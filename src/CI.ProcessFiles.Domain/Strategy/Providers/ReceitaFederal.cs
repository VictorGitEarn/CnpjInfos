using CI.ProcessFiles.Domain.Business;
using CI.ProcessFiles.Domain.Business.DTOs;
using System;
using System.Collections.Generic;
using System.IO;

namespace CI.ProcessFiles.Domain.Strategy.Providers
{
    public class ReceitaFederal : AbstractReader
    {
        private readonly string CompanyIndicator = "1";
        private readonly string PartnerIndicator = "2";
        private Company CompanyToAdd = null;

        public override List<Company> ReadCompaniesFromFile(string path)
        {
            var companies = new List<Company>();

            var fileToRead = Directory.GetFiles(path);

            foreach (var file in fileToRead)
            {
                var lines = File.ReadLines(file);

                foreach (var line in lines)
                {
                    if (line.Length != 1200)
                        throw new ApplicationException($"Arquivo de nome {file} com formato incorreto");

                    if (line.Substring(0, 1) != CompanyIndicator && line.Substring(0, 1) != PartnerIndicator)
                        continue;

                    CompanyToAdd = ShouldCreateCompany(line, companies);
                    
                    if (line.Substring(0, 1) == PartnerIndicator)
                        CompanyToAdd.AddPartner(CreatePartner(line));
                }
            }

            if (CompanyToAdd != null && companies.Count == 0)
                companies.Add(CompanyToAdd);

            return companies;
        }

        private Company ShouldCreateCompany(string line, List<Company> companies)
        {
            if (line.Substring(0, 1) != CompanyIndicator)
                return CompanyToAdd;

            if (CompanyToAdd != null)
                companies.Add(CompanyToAdd);

            CompanyToAdd = new Company();

            var companyData = MapCopmpanyToDTO(line);

            CompanyToAdd.AddNames(companyData.Name, companyData.TradeName);
            CompanyToAdd.AddCompanyType(companyData.CompanyType);
            CompanyToAdd.AddSocialInfos(companyData.SocialSecurity, companyData.SocialCapital);
            CompanyToAdd.AddSituationAndDate(companyData.SituationType, companyData.SituationDate);
            CompanyToAdd.AddCEP(companyData.CEP);

            return CompanyToAdd;
        }

        private Partner CreatePartner(string line)
        {
            var partnerData = MapPartnerToDTO(line);

            var partner = new Partner();

            partner.AddName(partnerData.PartnerName);
            partner.AddPartnerType(partnerData.PartnerType);
            partner.AddTaxNumber(partnerData.TaxNumber);

            return partner;
        }

        protected override ReadFileDTO MapCopmpanyToDTO(string line)
        {
            return new ReadFileDTO
            {
                CompanyType = line.Substring(17, 1),
                SituationType = line.Substring(223, 2),
                SituationDate = line.Substring(225, 8),
                Name = line.Substring(18, 150).Trim(),
                TradeName = line.Substring(168, 55).Trim(),
                SocialSecurity = line.Substring(3, 14),
                SocialCapital = line.Substring(891, 14),
                CEP = line.Substring(674, 8)
            };
        }

        protected override ReadFileDTO MapPartnerToDTO(string line)
        {
            return new ReadFileDTO
            {
                PartnerName = line.Substring(18, 150).Trim(),
                PartnerType = line.Substring(17, 1),
                TaxNumber = line.Substring(168, 14)
            };
        }
    }
}