using System;
using System.Diagnostics;
using MyPhotoshop.Filters;
using MyPhotoshop.Filters.Parameters;

namespace PhotoshopProfiler
{
    internal class Program
    {
        public static void Test(Func<double[], LighteningParameters> function, int n)
        {
            var args = new double[] { 0, 1 };
            function(args);

            var watch = new Stopwatch();
            watch.Start();
            for (var i = 0; i < n; i++)
            {
                function(args);
            }

            watch.Stop();
            var ellapsedTime = watch.ElapsedMilliseconds * 1000;
            Console.WriteLine($"Полное время выполнения за {n} итераций: {(double)ellapsedTime} милисекунд;\n" +
                              $"Среднее время выполнения: {ellapsedTime / (double)n} милисекунд;\n\n");
        }

        public static void Main(string[] args)
        {
            var handler = new SimpleParametersHandler<LighteningParameters>();
            Test(values => handler.CreateParameters(values), 1000000);

            var staticHandler = new StaticParameterHandler<LighteningParameters>();
            Test(values => staticHandler.CreateParameters(values), 1000000);

            var expressionHandler = new ExpressionParameterHandler<LighteningParameters>();
            Test(values => expressionHandler.CreateParameters(values), 10000000);

            Test(values => new LighteningParameters { Coefficient = values[0] }, 10000000);
        }
    }
}