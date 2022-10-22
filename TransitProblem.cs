using System;
using System.Collections.Generic;
using System.IO;

namespace Perestanovka1;

public class TransitProblemInt
{
    public List<PointInt> Points { get; private set; }

    public TransitProblemInt(List<PointInt> points)
    {
        Points = points;
    }

    public TransitProblemInt(int count, int maxVal)
    {
        var rnd = new Random();
        var list = new List<PointInt>();
        for (int i = 0; i < count; i++)
            list.Add(new PointInt() { X = rnd.Next(maxVal), Y = rnd.Next(maxVal) });
        Points = list;
    }
    public void SavetoFile(string filename, IEnumerable<Path> paths)
    {

        using var writer = new StreamWriter(filename);
        foreach (var item in paths)
        {
            writer.WriteLine(item.ToString());
        }

        writer.WriteLine();
        writer.WriteLine($"#Index;X;Y;");
        for (int i = 0; i < Points.Count; i++)
        {
            writer.WriteLine($"{i};{Points[i].X};{Points[i].Y};");
        }

    }

    public static (TransitProblemInt, List<Path>) CreateFromFile(string filename)
    {
        List<Path> paths = new();
        List<string> linePaths = new();
        //Dictionary<int, (int, int)> points = new();
        List<PointInt> points = new();

        using var reader = new StreamReader(filename);
        bool isFirstLine = true;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrEmpty(line))
                continue;
            if (line.Contains("#Index"))
                isFirstLine = false;
            if (line[0] == '#')
                continue;
            if (isFirstLine)
            {
                //isFirstLine = false;
                linePaths.Add(line);
                //paths.Add(getPathByString(line));
            }
            else
            {
                var pointsArr = parseIntString(line);
                if (pointsArr.Length >= 3)
                {
                    points.Add(new PointInt() { X = pointsArr[1], Y = pointsArr[2] });
                    //points.Add(pointsArr[0], (pointsArr[1], pointsArr[2]));
                }
            }
        }
        TransitProblemInt problem = new(points);

        foreach (var item in linePaths)
            paths.Add(Path.GetPathByString(item, problem));
        return (problem, paths);
    }
    public static int[] parseIntString(string line, char delimetr = ';')
    {
        List<int> result = new();
        var str = line.Split(delimetr);
        foreach (var item in str)
            if (int.TryParse(item, out int chislo))
                result.Add(chislo);

        return result.ToArray();
    }
    public void PrintLn()
    {
        //using var writer = new StreamWriter(filename);
        Console.WriteLine($"Index;X;Y;");
        for (int i = 0; i < Points.Count; i++)
        {
            Console.WriteLine($"{i};{Points[i].X};{Points[i].Y};");
        }

    }
    public double Length(List<int> seq)
    {
        foreach (var item in seq)
        {
            if (item >= Points.Count)
                throw new IndexOutOfRangeException($"double Length(List<pointInt> points, List<int> seq) item={item} points.count={Points.Count}");
        }

        double length = 0d;

        for (int i = 0; i < seq.Count; i++)
        {
            if (i == seq.Count - 1)
                length += Length(Points[seq[i]], Points[seq[0]]);
            else
                length += Length(Points[seq[i]], Points[seq[i + 1]]);
        }
        return length;

    }

    private static double Length(PointInt x1, PointInt x2)
    {
        return Math.Sqrt((x1.X - x2.X) * (x1.X - x2.X) + (x1.Y - x2.Y) * (x1.Y - x2.Y));
    }
    private static double Length(PointD x1, PointD x2)
    {
        return Math.Sqrt((x1.X - x2.X) * (x1.X - x2.X) + (x1.Y - x2.Y) * (x1.Y - x2.Y));
    }
    private static double Length(PointD x1, PointInt x2)
    {
        return Math.Sqrt((x1.X - x2.X) * (x1.X - x2.X) + (x1.Y - x2.Y) * (x1.Y - x2.Y));
    }

    public List<double> GetLengthsBeforPointAndSeqPoints(PointInt p, List<int> indexs)
    {
        var result = new List<double>();
        for (int i = 0; i < indexs.Count; i++)
        {
            result.Add(Length(p, Points[indexs[i]]));

        }
        return result;

    }
    public List<double> GetLengthsBeforPointAndSeqPoints(PointD p, List<int> indexs)
    {
        var result = new List<double>();
        for (int i = 0; i < indexs.Count; i++)
        {
            result.Add(Length(p, Points[indexs[i]]));
        }
        return result;
    }
    public PointD GetWeightAveragePoint(PointD p, int index, double weight1, double weight2)
    {
        var p2 = Points[index];
        return CenterMass.GetPoint(p, p2, weight1, weight2);
    }
    public PointD GetWeightAveragePoint(PointInt p, int index, double weight1, double weight2)
    {
        var p2 = Points[index];
        return CenterMass.GetPoint(p, p2, weight1, weight2);
    }

    public List<double> GetLengthsBeforPointAndSeqPointsMoveToCenterMass(PointD p, List<int> indexs, double w)
    {
        var points = new List<PointInt>();
        foreach (var index in indexs)
            points.Add(Points[index]);

        PointD cp = CenterMass.GetCentrePoint(points);
        var w1 = indexs.Count;
        var result = new List<double>();
        foreach (var index in indexs)
        {
            var pc_i = CenterMass.GetPoint(cp, Points[index], w1, w);
            result.Add(Length(p, pc_i));
        }
        return result;
    }


}


