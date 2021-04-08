using CI.ProcessFiles.Domain.Business;
using CI.ProcessFiles.Domain.Business.DTOs;
using System.Collections.Generic;

namespace CI.ProcessFiles.Domain.Strategy
{
    public abstract class AbstractReader : IReadProvider
    {
        public abstract List<Company> ReadCompaniesFromFile(string path);

        protected abstract ReadFileDTO MapCopmpanyToDTO(string line);
        protected abstract ReadFileDTO MapPartnerToDTO(string line);
    }
}
