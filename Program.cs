using System;
using System.Collections.Generic;

namespace Perestanovka1
{
    /*public struct PointInt
    {
        public int X;
        public int Y;
    }
    public struct PointD
    {
        public double X;
        public double Y;
    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            TransitProblemInt problem = new TransitProblemInt(11, 100);
            BruteForceTransportProblem solver = new BruteForceTransportProblem(problem);
            solver.AnalyzePerestanovki(0);
            Console.WriteLine("");
            problem.PrintLn();
            Console.WriteLine("Hello World!");

            CenterMassSolver solver2 = new CenterMassSolver(problem);
            //var path = solver2.FindPath(0, 3.0);
            //printLn(path, problem.Length(path));
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine($"W={i}");
                solver2.FindOptPath(i);
               
            }
            for (int i = 1; i < 10; i++)
            {
                var x = i / 10.0;
                Console.WriteLine($"W={x:g3}");
                solver2.FindOptPath(x);

            }
            //Console.WriteLine($"W={0.5}");
            //solver2.FindOptPath(0.5);
            Console.WriteLine($"W={2/3.0:g5}");
            solver2.FindOptPath(2 / 3.0);

            Console.WriteLine($"W={0.05:g5}");
            solver2.FindOptPath(0.05);

            Console.WriteLine($"OptMass");
            solver2.FindOptPathMass();
            //Console.WriteLine($"W={2.0}");
            //solver2.FindOptPath(2.0);
            //Console.WriteLine($"W={1.0}");
            //solver2.FindOptPath(1.0);
        }
        public static void printLn(List<int> path,double length)
        {
            foreach (var item in path)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine($"  - {length:f3}");
        }

       
    }

}

