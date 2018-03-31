using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compareitor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoices = new CompareitorGenerator().GenereateInvoices();

            using (var db = new Compareitor.EntityiFramework.CompareitorDbContext())
            {
                //db.Database.Delete();
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE INVOICELINES");
                //db.Database.ExecuteSqlCommand("delete from Invoices");
                //db.Configuration.AutoDetectChangesEnabled = false;
                db.Invoices.AddRange(invoices);
                db.SaveChanges();
            }
        }
    }
}
