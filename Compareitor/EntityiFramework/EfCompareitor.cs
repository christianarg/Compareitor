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

    public class EfCompareitor : CompareitorBase, ICompareitor
    {
        public override CompareitorResult Execute(List<Invoice> invoices, string aditionaleName = null)
        {
            var result = new CompareitorResult { Name = CreateName(aditionaleName) };

            using (var db = new CompareitorDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Invoices.AddRange(invoices);
                db.SaveChanges();
            }

            return result;
        }
    }
}
