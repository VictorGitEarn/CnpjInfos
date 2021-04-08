using CI.API.Rest.Domain.Business;
using CI.API.Rest.Domain.Data;
using CI.API.Rest.Extentions;
using CI.API.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CI.API.Rest.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    [Obsolete("This version is obsolete!")]
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
                Cnpj = company.SocialSecurity,
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
                    CpfCnpj = partner.TaxNumber,
                    IdentificadorSocio = GetPartnerType(partner.PartnerType)
                });
            }

            return model;
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
