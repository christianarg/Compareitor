using Compareitor.CommonModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compareitor
{
    public class Configuration
    {
        public static int LoopCount
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["LoopCount"];
                if (!string.IsNullOrEmpty(setting) && int.TryParse(setting, out int loopCount))
                {
                    return loopCount;
                }
                return 10000;   // default
            }
        }
    }

    public class CompareitorGenerator
    {
        public List<Invoice> GenereateInvoices() => InvoiceFactory.GenereateInvoices();
    }

    public class InvoiceFactory
    {
        public static List<Invoice> GenereateInvoices()
        {
            var result = new List<Invoice>();

            for (int i = 0; i < Configuration.LoopCount; i++)
            {
                var invoice = new Invoice
                {
                    Id = i,
                    CustomerName = RandomStringGenerator.RandomString(50),
                    InvoiceDate = DateTime.Now.AddDays(new Random().Next(30)),
                    InvoiceLines = new List<InvoiceLine>()
                };
                result.Add(invoice);
                for (int j = 0; j < 4; j++)
                {
                    invoice.InvoiceLines.Add(new InvoiceLine
                    {
                        ProductName = RandomStringGenerator.RandomString(150),
                        PricePerUnit = new Random().Next(500),
                        Quantity = new Random().Next(500)
                    });
                }

            }
            return result;
        }
    }

    public interface ICompareitor
    {
        void Setup();
        CompareitorResult Execute(List<Invoice> invoices, string aditionaleName = null);
    }

    public abstract class CompareitorBase : ICompareitor
    {
        public abstract CompareitorResult Execute(List<Invoice> invoices, string aditionaleName = null);

        public virtual void Setup() { }

        protected string CreateName(string aditionaleName)
        {
            return $"{this.GetType().Name}-{aditionaleName}";
        }
    }

    public class CompareitorResult
    {
        public string Name { get; set; }
        public TimeSpan WriteElapsed { get; set; }
        public TimeSpan ReadElapsed { get; set; }
    }
}
