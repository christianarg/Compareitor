using Compareitor.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compareitor
{
    public class Constants
    {
        public const int LoopCount = 10000;
    }

    public class CompareitorGenerator
    {
        public List<Invoice> GenereateInvoices()
        {
            var result = new List<Invoice>();

            for (int i = 0; i < Constants.LoopCount; i++)
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
        CompareitorResult Execute(string aditionaleName = null);
    }

    public class CompareitorResult
    {
        public string Name { get; set; }
        public TimeSpan WriteElapsed { get; set; }
        public TimeSpan ReadElapsed { get; set; }
    }
}
