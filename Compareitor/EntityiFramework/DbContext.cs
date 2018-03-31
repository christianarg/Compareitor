using Compareitor.CommonModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compareitor.EntityiFramework
{
    public class CompareitorDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }

    public class EfCompareitor : ICompareitor
    {
        public CompareitorResult Execute(string aditionaleName = null)
        {
            var result = new CompareitorResult { Name = $"{this.GetType().Name}-{aditionaleName}" };

            for (int i = 0; i < Constants.LoopCount; i++)
            {
                // write
            }

            for (int i = 0; i < Constants.LoopCount; i++)
            {
                // read
            }

            return result;
        }
    }

}
