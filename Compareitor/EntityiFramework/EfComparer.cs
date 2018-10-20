using Compareitor.CommonModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Compareitor.EntityiFramework
{
    public class CompareitorDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
    }

    public class EfComparer : PerformanceComparerBase, IPerformanceComparer
    {
        public override void Setup()
        {
            using (var db = new CompareitorDbContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM InvoiceLines");
                db.Database.ExecuteSqlCommand("DELETE FROM Invoices");
            }
        }

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

    public class EfComparerForeachWriteOneContext : EfComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var invoice in invoices)
                {

                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfComparerForeachWriteOneContextWithAutoDetectChanges : EfComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorDbContext())
            {
                foreach (var invoice in invoices)
                {

                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfComparerForeachWriteOneContextOneSaveChanges : EfComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorDbContext())
            {
                db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var invoice in invoices)
                {
                    db.Invoices.Add(invoice);
                }
                db.SaveChanges();
            }
        }
    }

    public class EfComparerForeachWrite : EfComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorDbContext())
                {
                    db.Configuration.AutoDetectChangesEnabled = false;
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfComparerForeachWriteAutoDetectChangedTrue : EfComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorDbContext())
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }
}
