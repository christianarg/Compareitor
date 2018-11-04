using Compareitor.CommonModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore

namespace Compareitor.EfCore
{
    public class CompareitorEfCoreDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Compareitor.EntityiFramework.CompareitorEfCoreDbContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }

    public class EfCoreComparer : PerformanceComparerBase, IPerformanceComparer
    {
        public override void Setup(List<Invoice> invoices)
        {
            using (var db = new CompareitorEfCoreDbContext())
            {
                db.Database.EnsureCreated();
                db.Database.ExecuteSqlCommand("DELETE FROM InvoiceLines");
                db.Database.ExecuteSqlCommand("DELETE FROM Invoices");
            }
            foreach (var invoice in invoices)
            {
                invoice.Id = 0;
                foreach (var invoiceLine in invoice.InvoiceLines)
                {
                    invoiceLine.Id = 0;
                }
            }
        }

        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorEfCoreDbContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;
                db.Invoices.AddRange(invoices);
                db.SaveChanges();
            }
        }

        protected override void ExecuteRead(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorEfCoreDbContext())
                {
                    var invoiceFromDb = db.Invoices.Include((i) => i.InvoiceLines).SingleOrDefault(i => i.Id == invoice.Id);
                }
            }
        }
    }

    public class EfCoreComparerForeachWriteOneContext : EfCoreComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorEfCoreDbContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                foreach (var invoice in invoices)
                {

                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfCoreComparerForeachWriteOneContextWithAutoDetectChanges : EfCoreComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorEfCoreDbContext())
            {
                foreach (var invoice in invoices)
                {

                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfCoreComparerForeachWriteOneContextOneSaveChanges : EfCoreComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            using (var db = new CompareitorEfCoreDbContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                foreach (var invoice in invoices)
                {
                    db.Invoices.Add(invoice);
                }
                db.SaveChanges();
            }
        }
    }

    public class EfCoreComparerForeachWrite : EfCoreComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorEfCoreDbContext())
                {
                    db.ChangeTracker.AutoDetectChangesEnabled = false;
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }

    public class EfCoreComparerForeachWriteAutoDetectChangedTrue : EfCoreComparer
    {
        protected override void ExecuteWrite(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                using (var db = new CompareitorEfCoreDbContext())
                {
                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                }
            }
        }
    }
}
