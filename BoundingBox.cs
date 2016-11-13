using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

public class BoundingBox
{
    public List<Point> points;
    public Point topLeft;
    public Point topRight;
    public Point bottomLeft;
    public Point bottomRight;

    public double Width
    {
        get { return Vector2.Distance(topLeft, topRight); }
    }

    public double Height
    {
        get { return Vector2.Distance(topLeft, bottomLeft); }
    }

    public double Area
    {
        get { return Width * Height; }
    }

    // Also known as 'Elongation': Ratio of the longest and shortest side.
    public double AspectRatio
    {
        get { return Math.Max(Width, Height) / Math.Min(Width, Height); }
    }

    public int Left
    {
        get { return Math.Min(topLeft.X, bottomLeft.X); }
    }

    public int Right
    {
        get { return Math.Max(topRight.X, bottomRight.X); }
    }

    public int Top
    {
        get { return Math.Max(topLeft.Y, topRight.Y); }
    }

    public int Bottom
    {
        get { return Math.Min(bottomLeft.Y, bottomRight.Y); }
    }

    public BoundingBox(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        this.topLeft = topLeft;
        this.topRight = topRight;
        this.bottomLeft = bottomLeft;
        this.bottomRight = bottomRight;

        points = new List<Point>() { topLeft, topRight, bottomLeft, bottomRight };
    }

    public void ColorBoundingBox(Color color)
    {
        foreach (Point p in points)
        {
            if (p.X < 0 || p.X >= ImageProcessing.imageProcessing.inputImage.Width - 1)
                continue;
            if (p.Y < 0 || p.Y >= ImageProcessing.imageProcessing.inputImage.Height - 1)
                continue;

            ImageProcessing.imageProcessing.image[p.X, p.Y] = color;
        }
    }
}
