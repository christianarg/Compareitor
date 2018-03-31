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

    public class EfCompareitor : PerformanceComparerBase, IPerformanceComparer
    {
        public override ComparerResult Execute(List<Invoice> invoices, string aditionaleName = null)
        {
            var result = new ComparerResult { Name = CreateName(aditionaleName) };
            var stopwatch = Stopwatch.StartNew();
            using (var db = new CompareitorDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                db.Invoices.AddRange(invoices);
                db.SaveChanges();
            }

            stopwatch.Stop();
            result.WriteElapsed = stopwatch.Elapsed;
            stopwatch.Restart();

            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorDbContext())
                {
                    var invoiceFromDb = db.Invoices.Include((i) => i.InvoiceLines).SingleOrDefault(i => i.Id == invoice.Id);
                }
            }

            stopwatch.Stop();
            result.ReadElapsed = stopwatch.Elapsed;
            return result;
        }
    }
}
