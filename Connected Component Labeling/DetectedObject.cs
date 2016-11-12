﻿using System.Collections.Generic;
using System.Drawing;
using System;

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
}
