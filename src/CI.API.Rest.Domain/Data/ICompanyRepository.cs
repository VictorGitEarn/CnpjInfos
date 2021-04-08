using CI.API.Rest.Domain.Business;
using CI.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Data
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company GetBySocialSecurity(string socialSecurity);
    }
}
