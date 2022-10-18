using System;
using System.Collections.Generic;
using System.Linq;

namespace Perestanovka1;

public class CenterMassSolver
{
    private TransitProblemInt _problem;

    public CenterMassSolver(TransitProblemInt problem)
    {
        _problem = problem;
    }

    public Path FindOptimumPath()
    {
        var AllResult = new List<Path>();

        for (int i = 1; i < 10; i++)
        {
            var path = this.FindOptPath(i);
            AllResult.Add(path);
        }
        for (int i = 1; i < 10; i++)
        {
            var x = i / 10.0;
            var path = this.FindOptPath(x);
            AllResult.Add(path);
        }
        {
            var path_23 = this.FindOptPath(2 / 3.0);
            var path_005 = this.FindOptPath(0.05);
            var path_Opt = this.FindOptPathMass();
            AllResult.Add(path_23);
            AllResult.Add(path_005);
            AllResult.Add(path_Opt);
        }
        AllResult.Sort();
        return AllResult.First();

    }

    public Path FindOptPath(double weight)
    {
        var AllResult = new List<Path>();
        for (int i = 0; i < _problem.Points.Count; i++)
        {
            var points_i = FindPath(i, weight);
            var path_i = new Path(points_i, _problem, $"OptPath w={weight}");
            AllResult.Add(path_i);
        }
        AllResult.Sort();
        return AllResult.First();
    }
    private Path FindOptPathMass()
    {
        List<Path> AllResult = new();
        for (int i = 0; i < _problem.Points.Count; i++)
        {
            var points_i = FindPathMass(i);
            var path_i = new Path(points_i, _problem, $"OptPathMass");
            AllResult.Add(path_i);
        }
        AllResult.Sort();
        return AllResult.First();
    }
    private List<int> FindPath(int startPoint, double weight)
    {
        var result = new List<int>();
        result.Add(startPoint);

        var startPointInt = _problem.Points[startPoint];

        PointD currentpoint = new PointD() { X = startPointInt.X, Y = startPointInt.Y };

        var AllPointIndex = new HashSet<int>();
        for (int i = 0; i < _problem.Points.Count; i++)
            AllPointIndex.Add(i);


        var other = new List<int>(AllPointIndex);
        other.Remove(startPoint);
        while (other.Count > 0)
        {
            var length = _problem.GetLengthsBeforPointAndSeqPoints(currentpoint, other);
            var min = length.Min();
            var minIndex1 = length.IndexOf(min);
            var minIndex2 = other[minIndex1];
            currentpoint = _problem.GetWeightAveragePoint(currentpoint, minIndex2, weight, 1.0);
            other.Remove(minIndex2);
            result.Add(minIndex2);
        }
        return result;
    }
    private List<int> FindPathMass(int startPoint)
    {
        var result = new List<int>();
        result.Add(startPoint);

        var startPointInt = _problem.Points[startPoint];
        PointD currentpoint = new PointD() { X = startPointInt.X, Y = startPointInt.Y };
        double sum = 1.0;
        var AllPointIndex = new HashSet<int>();
        for (int i = 0; i < _problem.Points.Count; i++)
            AllPointIndex.Add(i);


        var other = new List<int>(AllPointIndex);
        other.Remove(startPoint);
        double w2 = 1.0;
        while (other.Count > 0)
        {
            var length = _problem.GetLengthsBeforPointAndSeqPoints(currentpoint, other);
            var min = length.Min();
            var minIndex1 = length.IndexOf(min);
            var minIndex2 = other[minIndex1];
            currentpoint = _problem.GetWeightAveragePoint(currentpoint, minIndex2, sum, w2);
            sum++;
            w2 *= 1.2;
            other.Remove(minIndex2);
            result.Add(minIndex2);
        }
        return result;
    }

    public static void printLn(List<int> path, double length)
    {
        foreach (var item in path)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine($"  - {length:g5}");
    }
}

