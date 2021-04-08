using CI.API.Rest.Domain.Business;
using CI.API.Rest.Domain.Data;
using CI.API.Rest.Extentions;
using CI.API.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CI.API.Rest.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("v{version:apiVersion}")]
    public class CompanyController : MainController
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public ActionResult GetCompany(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, "[^0-9]+", "");

            if (!Validators.CnpjIsValid(cnpj))
            {
                AddRequestError("CNPJ inválido");
                return CustomResponse();
            }

            var company = MapCompanyToModel(cnpj);

            if (company == null) return NotFound();

            return CustomResponse(company);
        }

        private CompanyModel MapCompanyToModel(string cnpj)
        {
            var company = _companyRepository.GetBySocialSecurity(cnpj);

            if (company == null)
                return null;

            var model = new CompanyModel()
            {
                CapitalSocial = company.SocialCapital,
                Cep = company.CEP,
                Cnpj = FormatTaxNumber(company.SocialSecurity),
                DataSituacao = company.SituationDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                IdentificadorEmpresa = GetCompanyType(company.CompanyType),
                Nome = company.Name,
                NomeFantasia = company.TradeName,
                Situacao = GetSituation(company.Situation),
                Socios = new List<PartnerModel>()
            };

            foreach (var partner in company.Partners)
            {
                model.Socios.Add(new PartnerModel
                {
                    Nome = partner.Name,
                    CpfCnpj = FormatTaxNumber(partner.TaxNumber),
                    IdentificadorSocio = GetPartnerType(partner.PartnerType)
                });
            }

            return model;
        }

        private static string FormatTaxNumber(string taxNumber)
        {
            if (!taxNumber.Contains("***"))
                return Convert.ToUInt64(taxNumber).ToString(@"00\.000\.000\/0000\-00");
            else
            {
                return $"{taxNumber.Substring(3, 3)}.{taxNumber.Substring(6, 3)}.{taxNumber.Substring(9, 3)}-{taxNumber.Substring(12, 2)}";
            }
        }

        private static string GetCompanyType(CompanyType companyType)
        {
            switch (companyType)
            {
                case CompanyType.Headquarter:
                    return "Matriz";
                case CompanyType.Subsidiary:
                    return "Filial";
                default:
                    return "Desconhecido";
            }
        }

        private static string GetSituation(Situation situation)
        {
            switch (situation)
            {
                case Situation.Null:
                    return "Nulo";
                case Situation.Active:
                    return "Ativo";
                case Situation.Suspend:
                    return "Supenso";
                case Situation.Incapable:
                    return "Inapta";
                case Situation.Disabled:
                    return "Baixada";
                default:
                    return "Desconhecido";
            }
        }

        private static string GetPartnerType(PartnerType partnerType)
        {
            switch (partnerType)
            {
                case PartnerType.Social:
                    return "Pessoa jurídica";
                case PartnerType.Individual:
                    return "Pessoa física";
                case PartnerType.Foreign:
                    return "Pessoa estrangeira";
                default:
                    return "Desconhecido";
            }
        }
    }
}
