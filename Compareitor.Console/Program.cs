using Compareitor.EntityiFramework;
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
            var comparer = new PerformanceComparerApplication();
            comparer.PerformanceComparers.Add(new EfComparer());
            comparer.ExecuteAndGenerateResult();
        }
    }
}
