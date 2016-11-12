using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

public class BoundingBox
{
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
    }

    public void ColorBoundingBox(Color color)
    {
        // Todo: check if in range.
        ImageProcessing.imageProcessing.image[topLeft.X, topLeft.Y] = color;
        ImageProcessing.imageProcessing.image[topRight.X, topRight.Y] = color;
        ImageProcessing.imageProcessing.image[bottomLeft.X, bottomLeft.Y] = color;
        ImageProcessing.imageProcessing.image[bottomRight.X, bottomRight.Y] = color;
    }
}
