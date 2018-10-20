using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compareitor.CommonModel;
using Dapper;

namespace Compareitor.Dapper
{
    public class DapperComparer : PerformanceComparerBase
    {
        protected override void ExecuteRead(List<Invoice> invoices)
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            throw new NotImplementedException();
        }
    }
}
