using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Business
{
    public class Partner
    {
        public Partner() { }

        public Partner(string name, PartnerType partnerType, string taxNumber)
        {
            Name = name;
            PartnerType = partnerType;
            TaxNumber = taxNumber;
        }

        public string Name { get; private set; }

        public PartnerType PartnerType { get; private set; }

        public string TaxNumber { get; private set; }
    }
}
