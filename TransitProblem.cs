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

    public TransitProblemInt(int count,int maxVal)
    {                      
        var rnd = new Random();
        var list = new List<PointInt>();
        for (int i = 0; i < count; i++)
            list.Add(new PointInt() { X = rnd.Next(maxVal), Y = rnd.Next(maxVal) });
        Points = list;
    }
    public void SavetoFile(string filename)
    {
        using var writer = new StreamWriter(filename);
        writer.WriteLine($"Index;X;Y;");
        for (int i = 0; i < Points.Count; i++)
        {
            writer.WriteLine($"{i};{Points[i].X};{Points[i].Y};");
        }

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
        var x = (p.X * weight1 + p2.X * weight2) / (weight1 + weight2);
        var y = (p.Y * weight1 + p2.Y * weight2) / (weight1 + weight2);

        return new PointD() { X = x, Y = y };
    }
    public PointD GetWeightAveragePoint(PointInt p, int index, double weight1, double weight2)
    {
        var p2 = Points[index];
        var x = (p.X * weight1 + p2.X * weight2) / (weight1 + weight2);
        var y = (p.Y * weight1 + p2.Y * weight2) / (weight1 + weight2);

        return new PointD() { X = x, Y = y };
    }

}


