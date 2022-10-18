using System;
using System.Collections.Generic;
using Timer = System.Diagnostics.Stopwatch;
namespace Perestanovka1
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer time = new Timer();
            time.Start();
            TransitProblemInt problem = new TransitProblemInt(30, 100);
            //BruteForceTransportProblem solver = new BruteForceTransportProblem(problem);
            //var minPath = solver.AnalyzePerestanovki(0);
            time.Stop();
            printLn("");
            printLn($"Time for Analyze= {time.ElapsedMilliseconds} ms");
            printLn("");
            problem.PrintLn();

            var dt = DateTime.Now;
            var timeString = $"{dt.Month}_{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}";

            //Console.WriteLine("Hello World!");
            time.Restart();
            CenterMassSolver solver2 = new CenterMassSolver(problem);

            var path2 = solver2.FindOptimumPath();

            printLn(path2.ToString());
            time.Stop();
            printLn($"Time for Analyze= {time.ElapsedMilliseconds} ms");
            var pathes = new List<Path>() { path2 };
            //pathes.Add(minPath);
            problem.SavetoFile($"Data//Data{timeString}.dat", pathes);
        }
        public static void printLn(List<int> path, double length)
        {
            foreach (var item in path)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine($"  - {length:f3}");
        }

        public static void printLn(string s)
        {
            Console.WriteLine(s);
        }

    }

}

