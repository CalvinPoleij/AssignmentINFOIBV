using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

public class Vector2
{
    public float X;
    public float Y;

    public float Length
    {
        get { return (float)Math.Sqrt(X * X + Y * Y); }
    }

    public static Vector2 Zero
    {
        get { return new Vector2(0, 0); }
    }

    public Vector2(float X, float Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public Vector2 Normalize()
    {
        X /= Length;
        Y /= Length;

        return this;
    }

    public static Vector2 Orthogonal(Vector2 a)
    {
        return new Vector2(a.Y, -a.X);
    }

    public static float Dot(Vector2 a, Vector2 b)
    {
        return a.X * b.X + a.Y * b.Y;
    }

    // Cross product between two Vectors. If this returns zero, the two vectors are parallel.
    public static float Cross(Vector2 a, Vector2 b)
    {
        return a.X * b.Y - b.X * a.Y;
    }

    public static Vector2 Difference(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2 Difference(Point a, Point b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }

    public static double Distance(Vector2 a, Vector2 b)
    {
        float deltaX = a.X - b.X;
        float deltaY = a.Y - b.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public static double Distance(Point a, Point b)
    {
        float deltaX = a.X - b.X;
        float deltaY = a.Y - b.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public static Vector2 operator -(Vector2 a)
    {
        return new Vector2(-a.X, -a.Y);
    }

    public static Vector2 operator *(Vector2 a, float b)
    {
        return new Vector2(b * a.X, b * a.Y);
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.X - b.X, a.Y - b.Y);
    }
}
