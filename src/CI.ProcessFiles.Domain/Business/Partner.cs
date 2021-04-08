using System;

namespace CI.ProcessFiles.Domain.Business
{
    public class Partner
    {
        public string Name { get; private set; }

        public PartnerType PartnerType { get; private set; }

        public string TaxNumber { get; private set; }

        public void AddName(string name)
        {
            Name = name;
        }

        public void AddPartnerType(string partnerType)
        {
            if (!Enum.TryParse(partnerType, true, out PartnerType type))
                throw new ApplicationException($"Não foi possível converter o identificador de sócio: {partnerType}");

            PartnerType = type;
        }

        public void AddTaxNumber(string taxNumber)
        {
            TaxNumber = taxNumber;
        }
    }
}
