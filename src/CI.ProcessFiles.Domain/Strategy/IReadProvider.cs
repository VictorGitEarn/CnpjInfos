using CI.ProcessFiles.Domain.Business;
using System.Collections.Generic;

namespace CI.ProcessFiles.Domain.Strategy
{
    public interface IReadProvider
    {
        List<Company> ReadCompaniesFromFile(string path);
    }
}
