using Compareitor.CommonModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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

    public class EfComparer : PerformanceComparerBase, IPerformanceComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Invoices.AddRange(invoices);
                db.SaveChanges();
            }
        }

        protected override void ExecuteRead(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorDbContext())
                {
                    var invoiceFromDb = db.Invoices.Include((i) => i.InvoiceLines).SingleOrDefault(i => i.Id == invoice.Id);
                }
            }
        }
    }
}
