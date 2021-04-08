using System;
using System.Collections.Generic;

namespace CI.API.Rest.Models
{
    public class CompanyModel
    {
        public string Cnpj { get; set; }

        public string IdentificadorEmpresa { get; set; }

        public string Nome { get; set; }

        public string NomeFantasia { get; set; }

        public string CapitalSocial { get; set; }

        public string Situacao { get; set; }

        public string DataSituacao { get; set; }

        public string Cep { get; set; }

        public List<PartnerModel> Socios { get; set; }
    }
}
