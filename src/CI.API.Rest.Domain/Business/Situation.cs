using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI.API.Rest.Domain.Business
{
    public enum Situation
    {
        Null = 01,
        Active = 02,
        Suspend = 03,
        Incapable = 04,
        Disabled = 08
    }
}
