using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;


public class DetectedObject
{
    public int id;
    public List<Point> pixels = new List<Point>();
    public List<Point> perimeterPixels = new List<Point>();

    
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

    // Give each pixel belonging to the bounding box of this object a color.
    public void ColorBoundingBox(Color color)
    {
        Rectangle boundingBox = AxisAllignedBoundingBox();

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

    //bepaal de compactness van het object c=l(^2)/(4PiA)
    public int Compactness
    {
        get { return (Perimeter * 2) / (Convert.ToInt32(Math.PI * 4) * Area); }

    }

    public int AreaBox//Vul bounding box in
    { get; }

    //bepaal de rectangularity van een object
    public int Rectangularity
    {
        get { return Area / AreaBox; }

    }

    // Determine the axis alligned bounding box of this object.
    public Rectangle AxisAllignedBoundingBox()
    {
        int minX = perimeterPixels.Min(p => p.X);
        int maxX = perimeterPixels.Max(p => p.X);
        int minY = perimeterPixels.Min(p => p.Y);
        int maxY = perimeterPixels.Max(p => p.Y);

        // Return the bounding box.
        return new Rectangle(minX, minY, maxX - minX, maxY - minY);
    }

    public Rectangle MinimumBoundingBox()
    {
        return AxisAllignedBoundingBox();
    }

    // Calculate the Convex Hull using Jarvis March algorithm.
    public List<Point> ConvexHull()
    {
        if (perimeterPixels.Count < 3)
        {
            Console.WriteLine("Jarvis March requires 3 points to calculate the Convex Hull");
            return null;
        }

        List<Point> convexHullPoints = new List<Point>();
        Point hullPoint = perimeterPixels.Find(p => p.X == perimeterPixels.Min(p2 => p2.X));
        Point endPoint;

        do
        {
            convexHullPoints.Insert(0, hullPoint);
            ImageProcessing.imageProcessing.image[hullPoint.X, hullPoint.Y] = Color.Yellow;
            endPoint = perimeterPixels[0];

            for (int i = 1; i < perimeterPixels.Count; i++)
            {
                Point p = perimeterPixels[i];

                // Check if the current perimeter pixel is on the left side of the line from hullPoint to endPoint.
                float distanceToLine = (endPoint.X - hullPoint.X) * (p.Y - hullPoint.Y) -
                                       (endPoint.Y - hullPoint.Y) * (p.X - hullPoint.X);

                if (endPoint == hullPoint || distanceToLine > 0)
                    endPoint = p;
            }

            hullPoint = endPoint;
        }
        while (convexHullPoints[convexHullPoints.Count - 1] != endPoint);

        return convexHullPoints;
    }

    private double Distance(Point A, Point B)
    {
        return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
    }
}
