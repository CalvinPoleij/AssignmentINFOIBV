using System.Collections.Generic;
using System.Drawing;

public class DetectedObject
{
    public int id;
    public List<Point> pixels = new List<Point>();

    // Properties
    public int Area
    {
        get { return pixels.Count; }
    }

    // Constructor for initialization.
    public DetectedObject(int id)
    {
        this.id = id;
    }

    // Adds a pixel to this DetectedObject list of pixels.
    public void AddPixel(int x, int y)
    {
        pixels.Add(new Point(x, y));
    }

    // Give each pixel belonging to this object a color.
    public void ColorObject(Color color)
    {
        foreach (Point point in pixels)
            ImageProcessing.imageProcessing.image[point.X, point.Y] = color;
    }
}
