using System.Collections.Generic;
using System.Drawing;
using System;
using System.Linq;

public class DetectedObject
{
    public int id;
    public List<Point> pixels = new List<Point>();
    public List<Point> perimeterPixels = new List<Point>();

    public BoundingBox minimumBoundingBox;
    public BoundingBox axisAllignedBoundingBox;

    // Properties
    public int Area
    {
        get { return pixels.Count; }
    }

    public int Perimeter
    {
        get { return perimeterPixels.Count; }
    }

    public float AreaPerimeterRatio     //verhouding van omtrek tot oppervlakte
    {
        get
        {
            if (Perimeter == 0)
                return 0;
            else
                return Area / Perimeter;
        }
    }

    public double Rectangularity        //bereken rectangularity, gebruikt in objectdetectie
    {
        get
        {
            BoundingBox mbb = MinimumBoundingBox();
            if (mbb != null)
                return Area / mbb.Area;
            else
                return 0;
        }
    }

    public double Elongation            //bereken elongation, gebruik in objectdetectie
    {
        get { return MinimumBoundingBox().AspectRatio; }
    }

    // Determine the compactness of the object, using the formula c = l(^2)/(4Pi * A)
    public double Compactness
    {
        get { return (Perimeter * Perimeter) / ((Math.PI * 4) * Area); }
    }

    // Constructor for initialization.
    public DetectedObject(int id)
    {
        this.id = id;
    }

    // Erodes an object.
    public void Erosion(int erosionLevel = 1)
    {
        for (int i = 0; i < erosionLevel; i++)
        {
            foreach (Point pixel in perimeterPixels)
            {
                pixels.Remove(pixel);
                ImageProcessing.imageProcessing.image[pixel.X, pixel.Y] = ImageProcessing.imageProcessing.backgroundColor;
            }

            perimeterPixels.Clear();

            foreach (Point pixel in pixels)
            {
                if (ImageProcessing.imageProcessing.CheckPerimeterPixel(pixel.X, pixel.Y))
                    perimeterPixels.Add(pixel);
            }
        }
    }

    // Dilates an object.
    public void Dilation(int dilationLevel = 1)
    {
        List<Point> toBeRemoved = new List<Point>();
        List<Point> toBeAdded = new List<Point>();

        // Repeat the dilation process for each dilationLevel.
        for (int i = 0; i < dilationLevel; i++)
        {
            // Per perimeter pixel, make all neighbouring background pixels a foreground pixel (diagonals excluded).
            foreach (Point p in perimeterPixels)
            {
                foreach (Point n in ImageProcessing.imageProcessing.GetPixelNeighbours(p.X, p.Y))
                    if (ImageProcessing.imageProcessing.image[n.X, n.Y] == ImageProcessing.imageProcessing.backgroundColor)
                        toBeAdded.Add(n);
                toBeRemoved.Add(p);
            }

            foreach (Point p in toBeRemoved)
                perimeterPixels.Remove(p);

            foreach (Point p in toBeAdded)
            {
                ImageProcessing.imageProcessing.image[p.X, p.Y] = ImageProcessing.imageProcessing.foregroundColor;
                perimeterPixels.Add(p);
                pixels.Add(p);
            }

            // Reset list variables.
            toBeRemoved.Clear();
            toBeAdded.Clear();
        }
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
        BoundingBox boundingBox = AxisAllignedBoundingBox();
        if (boundingBox == null)
            return;

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

    // Determine the axis alligned bounding box of this object.
    public BoundingBox AxisAllignedBoundingBox()
    {
        if (perimeterPixels.Count == 0)
            return null;

        if (axisAllignedBoundingBox != null)
            return axisAllignedBoundingBox;

        int minX = perimeterPixels.Min(p => p.X);
        int maxX = perimeterPixels.Max(p => p.X);
        int minY = perimeterPixels.Min(p => p.Y);
        int maxY = perimeterPixels.Max(p => p.Y);

        axisAllignedBoundingBox = new BoundingBox(new Point(minX, maxY), new Point(maxX, maxY), new Point(minX, minY), new Point(maxX, minY));
        return axisAllignedBoundingBox;
    }

    // Calculate the Minimum Bounding Box of the object.
    public BoundingBox MinimumBoundingBox()
    {
        if (minimumBoundingBox != null)
            return minimumBoundingBox;

        BoundingBox mmb = RotatingCalipers(ConvexHull());

        if (mmb == null)
            return AxisAllignedBoundingBox();
        else
            return mmb;
    }

    // Calculate the Convex Hull using Jarvis March algorithm (Convex hull = a closed chain of perimeter points, needed for the minimum bouding box).
    public List<Point> ConvexHull(bool color = true)
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

        // Color the found convex hull points (mainly for debugging purposes).
        if (color)
        {
            foreach (Point point in convexHullPoints)
                ImageProcessing.imageProcessing.image[point.X, point.Y] = Color.LimeGreen;
        }

        return convexHullPoints;
    }

    // Use the Convex Hull to calculate the Minimum Bounding Box, via the Rotating Calipers algorithm.
    public BoundingBox RotatingCalipers(List<Point> convexHull)
    {
        // If there is no convex hull, return, as this method cannot be executed properly.
        if (convexHull == null || convexHull.Count == 0)
            return null;

        BoundingBox minimumBoundingBox = null;
        double smallestBoundingBoxArea = int.MaxValue;

        List<Vector2> edgeDirections = new List<Vector2>();

        // Calculate convex hull edge directions and add them to the list.
        for (int i = 0; i < convexHull.Count; i++)
        {
            Point a = convexHull[(i + 1) % convexHull.Count];
            Point b = convexHull[i];

            Vector2 edgeDirection = Vector2.Difference(a, b);
            edgeDirections.Add(edgeDirection.Normalize());
        }

        // Determine the 'extreme points' of the convex hull.
        int minXIndex = convexHull.FindIndex(p => p.X == convexHull.Min(p2 => p2.X));
        int maxXIndex = convexHull.FindIndex(p => p.X == convexHull.Max(p2 => p2.X));
        int minYIndex = convexHull.FindIndex(p => p.Y == convexHull.Min(p2 => p2.Y));
        int maxYIndex = convexHull.FindIndex(p => p.Y == convexHull.Max(p2 => p2.Y));

        // Convert the extreme points to Vector2.
        Vector2 minX = new Vector2(convexHull[minXIndex].X, convexHull[minXIndex].Y);
        Vector2 maxX = new Vector2(convexHull[maxXIndex].X, convexHull[maxXIndex].Y);
        Vector2 minY = new Vector2(convexHull[minYIndex].X, convexHull[minYIndex].Y);
        Vector2 maxY = new Vector2(convexHull[maxYIndex].X, convexHull[maxYIndex].Y);

        // Directional Vectors at the start of the Rotating Calipers algorithm.
        Vector2 topSideDirection = new Vector2(-1, 0);          // Direction: left
        Vector2 bottomSideDirection = new Vector2(1, 0);        // Direction: right
        Vector2 rightSideDirection = new Vector2(0, 1);         // Direction: up
        Vector2 leftSideDirection = new Vector2(0, -1);         // Direction: down

        // Rotating Calipers algorithm.
        for (int i = 0; i < convexHull.Count; i++)
        {
            // Per side (left, right, top bottom), calculate its angle.
            List<double> angles = new List<double>()
            {
                Math.Acos(Vector2.Dot(leftSideDirection, edgeDirections[minXIndex])),
                Math.Acos(Vector2.Dot(rightSideDirection, edgeDirections[maxXIndex])),
                Math.Acos(Vector2.Dot(topSideDirection, edgeDirections[maxYIndex])),
                Math.Acos(Vector2.Dot(bottomSideDirection, edgeDirections[minYIndex]))
            };

            // Check which of the four sides has the smallest angle.
            int smallestAngleIndex = angles.FindIndex(x => x == angles.Min());

            // Rotate the dynamically adjustable Caliper around the convex hull, depending on which side has the smallest angle.
            switch (smallestAngleIndex)
            {
                case 0:
                    leftSideDirection = edgeDirections[minXIndex];
                    rightSideDirection = -leftSideDirection;
                    topSideDirection = Vector2.Orthogonal(leftSideDirection);
                    bottomSideDirection = -topSideDirection;
                    minXIndex = (minXIndex + 1) % convexHull.Count;
                    break;
                case 1:
                    rightSideDirection = edgeDirections[maxXIndex];
                    leftSideDirection = -rightSideDirection;
                    topSideDirection = Vector2.Orthogonal(leftSideDirection);
                    bottomSideDirection = -topSideDirection;
                    maxXIndex = (maxXIndex + 1) % convexHull.Count;
                    break;
                case 2:
                    topSideDirection = edgeDirections[maxYIndex];
                    bottomSideDirection = -topSideDirection;
                    leftSideDirection = Vector2.Orthogonal(bottomSideDirection);
                    rightSideDirection = -leftSideDirection;
                    maxYIndex = (maxYIndex + 1) % convexHull.Count;
                    break;
                case 3:
                    bottomSideDirection = edgeDirections[minYIndex];
                    topSideDirection = -bottomSideDirection;
                    leftSideDirection = Vector2.Orthogonal(bottomSideDirection);
                    rightSideDirection = -leftSideDirection;
                    minYIndex = (minYIndex + 1) % convexHull.Count;
                    break;
            }

            // Determine new bounding box, formed by the intersections of the directional vectors.
            Point topLeft = IntersectionPoint(minX, leftSideDirection, maxY, topSideDirection);
            Point topRight = IntersectionPoint(maxX, rightSideDirection, maxY, topSideDirection);
            Point bottomLeft = IntersectionPoint(minY, bottomSideDirection, minX, leftSideDirection);
            Point bottomRight = IntersectionPoint(minY, bottomSideDirection, maxX, rightSideDirection);

            // Compare the new bounding box to the smallest found bounding box.
            double boundingBoxArea = Vector2.Distance(topLeft, topRight) * Vector2.Distance(topLeft, bottomLeft);
            if (boundingBoxArea < smallestBoundingBoxArea)
            {
                minimumBoundingBox = new BoundingBox(topLeft, topRight, bottomLeft, bottomRight);
                smallestBoundingBoxArea = boundingBoxArea;
            }
        }

        // Save the found minimum bounding box as a field variable, for efficiency.
        this.minimumBoundingBox = minimumBoundingBox;

        return minimumBoundingBox;
    }

    // Calculate the point of intersection of two line segments. (Used in RotatingCalipers() method)
    private Point IntersectionPoint(Vector2 aPos, Vector2 aDir, Vector2 bPos, Vector2 bDir)
    {
        Vector2 delta = bPos - aPos;
        Vector2 newDirection = aDir * ((delta.X * bDir.Y - delta.Y * bDir.X) / Vector2.Cross(aDir, bDir));

        return new Point((int)(aPos.X + newDirection.X), (int)(aPos.Y + newDirection.Y));
    }
}