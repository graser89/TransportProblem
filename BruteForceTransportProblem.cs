using System;
using System.Collections.Generic;
using System.Linq;

namespace Perestanovka1;

public class BruteForceTransportProblem
{
    private readonly TransitProblemInt _problem;

    public BruteForceTransportProblem(TransitProblemInt problem)
    {
        _problem = problem;
    }

    public void Analyze()
    {
        var allpath = CreatePerestanovki(_problem.Points.Count, 0);
        var (minInd, min) = AnalyzeSequences(allpath.ToList());
    }
    public (int minIndex, double min) AnalyzeSequences(List<List<int>> elements)
    {
        double min = double.MaxValue;
        int min_index = 0;

        for (int i = 0; i < elements.Count; i++)
        {
            var elem = elements[i];
            var length = _problem.Length(elem);

            if (length < min)
            {
                min = length;
                min_index = i;
            }    
            foreach (var item in elem)
                //Console.Write($"{item} ");
                print($"{item} ");

            printLn($"  - {length:g5}");
        }

        printLn("");
        printLn("");
        printLn($"Minimum Result");
        var elem2= elements[min_index];            
        foreach (var item in elem2)
            //Console.Write($"{item} ");
            print($"{item} ");

        printLn($"  - {min:g5}");
        return (min_index, min);
    }
    private static void print(string s)
    {
        Console.Write(s);
    }
    public static void printLn(string s)
    {
        Console.WriteLine(s);
    }
    static void printLn(List<int> elem)
    {
        foreach (var item in elem)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }
    static void printLn(List<int> elem, double length)
    {
        foreach (var item in elem)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine($" - {length:g5}");
    }

    static void printLn(PointInt item)
    {
        Console.WriteLine($"point x={item.X} y={item.Y}");
    }
    static void printLn(PointD item)
    {
        Console.WriteLine($"point x={item.X:f3} y={item.Y:f3}");
    }

    public void AnalyzePerestanovki(int from = -1)
    {
        int count = _problem.Points.Count;
        HashSet<int> initSet = new();
        for (int i = 0; i < count; i++)
            initSet.Add(i);

        HashSet<List<int>> firstElem = new();
        if (from < 0)        
            for (int i = 0; i < count; i++)            
                firstElem.Add(new List<int>() { i });                    
        else        
            firstElem.Add(new List<int>() { from });
        

        double min = double.MaxValue;
        List<int> min_Path = new();

        Stack<List<int>> stack = new();
        foreach (var item in firstElem)            
            stack.Push(item);
        int countPer = 0;
        while (stack.Count>0)
        {
            var elem = stack.Pop();
            if (elem.Count < count)
            {
                var dop = Dopolnenie(initSet, elem);
                foreach (var item in dop)                    
                    stack.Push(item);                    
            }
            else if (elem.Count==count)
            {
                var length = _problem.Length(elem);
                //printLn(elem, length);
                countPer++;
                if (length < min)
                {
                    min = length;
                    min_Path = elem;
                    printLn(elem, length);
                }
                //if (countPer % 1000 == 0)
                //    Console.WriteLine(countPer);
            }
        }
        printLn("");
        printLn("");
        printLn($"Minimum Result");
        printLn(min_Path, min);

    }
    private static HashSet<List<int>> CreatePerestanovki(int count, int from = -1)
    {
        HashSet<int> initSet = new();
        for (int i = 0; i < count; i++)
            initSet.Add(i);
        HashSet<List<int>> firstElem = new();
        if (from < 0)            
            for (int i = 0; i < count; i++)                
                firstElem.Add(new List<int>() { i });                            
        else            
            firstElem.Add(new List<int>() { from });
        
        var prevResult = firstElem;
        var nextResult = new HashSet<List<int>>();
        for (int i = 0; i < count - 1; i++)
        {
            nextResult = new HashSet<List<int>>();
            foreach (var item in prevResult)
            {
                var dop = Dopolnenie(initSet, item);
                nextResult.UnionWith(dop);
            }
            prevResult = nextResult;
        }
        return nextResult;
    }

    static HashSet<List<int>> Dopolnenie(HashSet<int> initSet, List<int> element)
    {
        if (element.Count >= initSet.Count)
            return new HashSet<List<int>>() { element };
        var notUsed = new HashSet<int>(initSet);

        notUsed.ExceptWith(element);

        var result = new HashSet<List<int>>();
        foreach (var item in notUsed)
        {
            var new_elem = new List<int>(element);
            new_elem.Add(item);
            result.Add(new_elem);
        }
        return result;
    }
}

