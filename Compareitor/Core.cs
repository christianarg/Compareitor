using Compareitor.CommonModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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

    public class PerformanceComparerApplication
    {
        public List<IPerformanceComparer> PerformanceComparers { get; set; } = new List<IPerformanceComparer>();
        public List<Invoice> GenereateInvoices() => InvoiceFactory.GenereateInvoices();

        public List<ComparerResult> ExecuteAndGenerateResult()
        {
            var results = ExecuteComparison();
            string path = Path.Combine(Environment.CurrentDirectory, "result.csv");
            if (!File.Exists(path))
            {
                File.WriteAllText(path, $"{nameof(ComparerResult.Name)},{nameof(ComparerResult.WriteElapsed)},{nameof(ComparerResult.ReadElapsed)}{Environment.NewLine}");
            }

            foreach (var result in results)
            {
                File.AppendAllText(path, $"{result.Name},{result.WriteElapsed},{result.ReadElapsed}{Environment.NewLine}");
            }
            return results;
        }

        public List<ComparerResult> ExecuteComparison()
        {
            var result = new List<ComparerResult>();
            var invoices = GenereateInvoices();
            foreach (var comparer in PerformanceComparers)
            {
                comparer.Setup();
                result.Add(comparer.Execute(invoices));
            }

            return result;
        }
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

    public interface IPerformanceComparer
    {
        void Setup();
        ComparerResult Execute(List<Invoice> invoices, string aditionaleName = null);
    }

    public abstract class PerformanceComparerBase : IPerformanceComparer
    {
        public virtual void Setup() { }

        public virtual ComparerResult Execute(List<Invoice> invoices, string aditionaleName = null)
        {
            var result = new ComparerResult { Name = CreateName(aditionaleName) };
            var stopwatch = Stopwatch.StartNew();

            ExecuteWrite(invoices);

            stopwatch.Stop();
            result.WriteElapsed = stopwatch.Elapsed;
            stopwatch.Restart();

            ExecuteRead(invoices);

            stopwatch.Stop();
            result.ReadElapsed = stopwatch.Elapsed;
            return result;
        }

        protected abstract void ExecuteRead(List<Invoice> invoices);

        protected abstract void ExecuteWrite(List<Invoice> invoices);

        protected string CreateName(string aditionaleName)
        {
            string name = $"{this.GetType().Name}";
            if (!string.IsNullOrEmpty(aditionaleName))
            {
                return $"{name}-{aditionaleName}";
            }
            return name;
        }
    }

    public class ComparerResult
    {
        public string Name { get; set; }
        public TimeSpan WriteElapsed { get; set; }
        public TimeSpan ReadElapsed { get; set; }
    }
}
