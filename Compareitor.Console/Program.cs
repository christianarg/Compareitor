using Compareitor.AzureBlobStorage;
using Compareitor.EfCore;
using Compareitor.EntityiFramework;
using System.Configuration;

namespace Compareitor.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            EfCoreVsEf();
        }

        private static void EfCoreVsEf()
        {
            var comparer = new PerformanceComparerApplication();
            comparer.PerformanceComparers.Add(new EfComparer());
            comparer.PerformanceComparers.Add(new EfCoreComparer());
            comparer.ExecuteAndGenerateResult();
        }

        private static void EfCoreComparer()
        {
            var comparer = new PerformanceComparerApplication();
            comparer.PerformanceComparers.Add(new EfCoreComparer());
            comparer.PerformanceComparers.Add(new EfCoreComparerForeachWriteOneContext());
            comparer.PerformanceComparers.Add(new EfCoreComparerForeachWriteOneContextWithAutoDetectChanges());
            comparer.PerformanceComparers.Add(new EfCoreComparerForeachWrite());
            comparer.PerformanceComparers.Add(new EfCoreComparerForeachWriteAutoDetectChangedTrue());
            comparer.PerformanceComparers.Add(new EfCoreComparerForeachWriteOneContextOneSaveChanges());

            comparer.ExecuteAndGenerateResult();
        }

        private static void EfComparer()
        {
            var comparer = new PerformanceComparerApplication();
            comparer.PerformanceComparers.Add(new EfComparer());
            comparer.PerformanceComparers.Add(new EfComparerForeachWriteOneContext());
            comparer.PerformanceComparers.Add(new EfComparerForeachWriteOneContextWithAutoDetectChanges());
            comparer.PerformanceComparers.Add(new EfComparerForeachWrite());
            comparer.PerformanceComparers.Add(new EfComparerForeachWriteAutoDetectChangedTrue());
            comparer.PerformanceComparers.Add(new EfComparerForeachWriteOneContextOneSaveChanges());

            comparer.ExecuteAndGenerateResult();
        }
    }
}
