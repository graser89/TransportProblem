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

    public List<int> FindOptPath(double weight)
    {
        double min = double.MaxValue;
        int indexMin = -1;
        var AllResult = new List<List<int>>();
        for (int i = 0; i < _problem.Points.Count; i++)
        {
            var path_i = FindPath(i, weight);
            var length = _problem.Length(path_i);
            AllResult.Add(path_i);
            if (length < min)
            {
                min = length;
                indexMin = i;
                printLn(path_i, length);
            }

        }
        return AllResult[indexMin];
    }
    public List<int> FindOptPathMass()
    {
        double min = double.MaxValue;
        int indexMin = -1;
        var AllResult = new List<List<int>>();
        for (int i = 0; i < _problem.Points.Count; i++)
        {
            var path_i = FindPathMass(i);
            var length = _problem.Length(path_i);
            AllResult.Add(path_i);
            if (length < min)
            {
                min = length;
                indexMin = i;
                printLn(path_i, length);
            }

        }
        return AllResult[indexMin];
    }
    public List<int> FindPath(int startPoint,double weight)
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
        while (other.Count>0)
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
    public List<int> FindPathMass(int startPoint)
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

