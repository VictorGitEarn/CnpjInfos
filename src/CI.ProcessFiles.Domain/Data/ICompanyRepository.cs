using CI.Core.Data;
using CI.ProcessFiles.Domain.Business;
using System.Collections.Generic;

namespace CI.ProcessFiles.Domain.Data
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetCompanyBySocialSecurity(string socialSecurity);

        void Save(Company company);

        void Delete(Company company);
    }
}
