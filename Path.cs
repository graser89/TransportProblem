using System;
using System.Collections.Generic;

namespace Perestanovka1;

public class Path : IComparable
{
    List<int> _points;
    public double Length { get; private set; }
    private TransitProblemInt _problem;
    public string Comment { get; private set; }
    public Path(List<int> points, TransitProblemInt problem, string comment = "")
    {
        _points = new List<int>(points);
        _problem = problem;
        this.Length = _problem.Length(_points);
        Comment = comment;
    }

    public override string ToString()
    {
        var res = "";
        foreach (var item in _points)
        {
            res += $"{item} ";
        }
        res += $" - {Length:g5} {Comment}";
        return res;
    }
    public static Path GetPathByString(string line, TransitProblemInt problem)
    {
        List<int> result = new();
        var line1Arr = line.Split('-');
        var line1 = line1Arr[0];
        var line2 = line1Arr[1];
        var str = line1.Split(' ');
        foreach (var item in str)
            if (int.TryParse(item, out int chislo))
                result.Add(chislo);
        //result.Add(result[0]);
        var path = new Path(result, problem, line2);

        return path;
    }

    int IComparable.CompareTo(object obj)
    {
        if (obj is Path otherP)
        {
            if (this.Length < otherP.Length)
                return -1;
            else if (Math.Abs(this.Length - otherP.Length) < 1e-6)
                return 0;
            else return 1;
        }
        else
            return -1;

    }
}

