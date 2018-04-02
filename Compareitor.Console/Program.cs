using Compareitor.AzureBlobStorage;
using Compareitor.EntityiFramework;
using System.Configuration;

namespace Compareitor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var comparer = new PerformanceComparerApplication();
            //comparer.PerformanceComparers.Add(new EfComparer());
            comparer.PerformanceComparers.Add(new BlobStorageComparer());

            comparer.ExecuteAndGenerateResult();
        }
    }
}
