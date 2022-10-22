
using System.Collections.Generic;
using System.Linq;

namespace Perestanovka1;

public static class CenterMass
{
    public static PointD GetPoint(PointD p1, PointD p2)
    {
        return new PointD() { X = (p1.X + p2.X) / 2d, Y = (p1.Y + p1.Y) / 2d };
    }
    public static PointD GetPoint(PointInt p1, PointD p2)
    {
        return new PointD() { X = (p1.X + p2.X) / 2d, Y = (p1.Y + p1.Y) / 2d };
    }
    public static PointD GetPoint(PointD p1, PointInt p2)
    {
        return new PointD() { X = (p1.X + p2.X) / 2d, Y = (p1.Y + p1.Y) / 2d };
    }
    public static PointD GetPoint(PointInt p1, PointInt p2)
    {
        return new PointD() { X = (p1.X + p2.X) / 2d, Y = (p1.Y + p1.Y) / 2d };
    }
    public static PointD GetPoint(PointD p1, PointD p2, double w1, double w2)
    {
        return new PointD() { X = (p1.X * w1 + p2.X * w2) / (w1 + w2), Y = (p1.Y * w1 + p1.Y * w2) / (w1 + w2) };
    }
    public static PointD GetPoint(PointInt p1, PointD p2, double w1, double w2)
    {
        return new PointD() { X = (p1.X * w1 + p2.X * w2) / (w1 + w2), Y = (p1.Y * w1 + p1.Y * w2) / (w1 + w2) };
    }
    public static PointD GetPoint(PointD p1, PointInt p2, double w1, double w2)
    {
        return new PointD() { X = (p1.X * w1 + p2.X * w2) / (w1 + w2), Y = (p1.Y * w1 + p1.Y * w2) / (w1 + w2) };
    }
    public static PointD GetPoint(PointInt p1, PointInt p2, double w1, double w2)
    {
        return new PointD() { X = (p1.X * w1 + p2.X * w2) / (w1 + w2), Y = (p1.Y * w1 + p1.Y * w2) / (w1 + w2) };
    }
    public static PointD GetCentrePoint(IEnumerable<PointD> points)
    {
        if (points == null || points.Count() == 0)
            return new PointD() { X = 0, Y = 0 };
        if (points.Count() == 1)
            return points.First();
        double sumX = 0d; double sumY = 0;
        foreach (var p_i in points)
        {
            sumX += p_i.X;
            sumY += p_i.Y;
        }
        double c = points.Count();
        return new PointD() { X = sumX / c, Y = sumY / c };

    }

    public static PointD GetCentrePoint(IEnumerable<PointInt> points)
    {
        if (points == null || points.Count() == 0)
            return new PointD() { X = 0, Y = 0 };
        if (points.Count() == 1)
        {
            var p1 = points.First();
            return new PointD() { X = p1.X, Y = p1.Y };
        }
        double sumX = 0d; double sumY = 0;
        foreach (var p_i in points)
        {
            sumX += p_i.X;
            sumY += p_i.Y;
        }
        double c = points.Count();
        return new PointD() { X = sumX / c, Y = sumY / c };

    }
}