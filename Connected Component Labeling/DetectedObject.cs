using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;

public class DetectedObject
{
    public int id;
    public List<Point> pixels = new List<Point>();
    public List<Point> perimeterPixels = new List<Point>();
    public Rectangle boundingBox;

    // Properties
    public int Area
    {
        get { return pixels.Count; }
    }

    public int Perimeter
    {
        get { return perimeterPixels.Count; }
    }

    public float AreaPerimeterRatio
    {
        get { return Area / Perimeter; }
    }

    // Constructor for initialization.
    public DetectedObject(int id)
    {
        this.id = id;
    }

    // Adds a pixel to this DetectedObject list of pixels.
    public void AddPixel(int x, int y, bool perimeter = false)
    {
        pixels.Add(new Point(x, y));

        if (perimeter)
            perimeterPixels.Add(new Point(x, y));
    }

    // Give each pixel belonging to this object a color.
    public void ColorObject(Color color)
    {
        foreach (Point point in pixels)
            ImageProcessing.imageProcessing.image[point.X, point.Y] = color;
    }

    // Give each pixel belonging to the perimeter of this object a color.
    public void ColorPerimeter(Color color)
    {
        foreach (Point point in perimeterPixels)
            ImageProcessing.imageProcessing.image[point.X, point.Y] = color;
    }

    public void ColorBoundingBox(Color color)
    {
        boundingBox = BoundingBox();

        for (int i = boundingBox.Left; i <= boundingBox.Right; i++)
        {
            ImageProcessing.imageProcessing.image[i, boundingBox.Top] = color;
            ImageProcessing.imageProcessing.image[i, boundingBox.Bottom] = color;
        }

        for (int j = boundingBox.Top; j <= boundingBox.Bottom; j++)
        {
            ImageProcessing.imageProcessing.image[boundingBox.Left, j] = color;
            ImageProcessing.imageProcessing.image[boundingBox.Right, j] = color;
        }
    }

    public Rectangle BoundingBox()
    {
        // If the boundingBox of this object has not already been determined, calculate it.
        int minX = pixels.Min(p => p.X);
        int maxX = pixels.Max(p => p.X);
        int minY = pixels.Min(p => p.Y);
        int maxY = pixels.Max(p => p.Y);

        boundingBox = new Rectangle(minX, minY, maxX - minX, maxY - minY);

        // Return the bounding box.
        return boundingBox;
    }
}
