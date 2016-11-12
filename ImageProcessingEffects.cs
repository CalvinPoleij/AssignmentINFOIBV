using System;
using System.Drawing;

// Partial class that handles all Image Processing Effects such as applying grayscale or making an image binary.
partial class ImageProcessing
{
    // Determine background- and foreground colors of a binary image.
    Color backgroundColor = Color.Black;
    Color foregroundColor = Color.White;

    // Creates a negative image. (included as an example in the template)
    private void ApplyNegative()
    {
        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                Color pixelColor = image[x, y];                                                                     // Get the pixel color at coordinate (x,y).
                Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);    // Negative color.
                image[x, y] = updatedColor;                                                                         // Set the new pixel color at coordinate (x,y).
            }
        }
    }

    // Creates a grayscaled image.
    private void ApplyGrayScale()
    {
        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                Color pixelColor = image[x, y];                                                                 // Get the pixel color at coordinate (x,y).

                // Apply greyscale.
                int grayScale = (int)(pixelColor.R * 0.299f + pixelColor.G * 0.587f + pixelColor.B * 0.114f);   // Calculates the correct grayscale value using the luma component.
                Color updatedColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);             // The new color, now with grayscale applied.

                image[x, y] = updatedColor;                                                                     // Set the new pixel color at coordinate (x,y).
            }
        }
    }

    // Applies thresholding to an image, given a threshold value. Returns a binary image.
    private void ApplyThreshold(bool negative = false)
    {
        double thresholdValue = thresholdTracker.Value;

        if (negative)
        {
            ApplyNegative();
            thresholdValue = thresholdTracker.Maximum - thresholdValue;
        }

        thresholdValue /= thresholdTracker.Maximum;

        // Convert image to black and white based on average brightness
        for (int y = 0; y < inputImage.Height; y++)
        {
            for (int x = 0; x < inputImage.Width; x++)
            {
                // Set this pixel to black or white based on threshold
                if (image[x, y].GetBrightness() >= thresholdValue)
                    image[x, y] = foregroundColor;
                else
                    image[x, y] = backgroundColor;
            }
        }
    }

    // Applies contrasting to an image, given a contrast value.
    private void ApplyContrast(double contrast)
    {
        contrast = Clamp(contrast, -100, 100);
        contrast = Math.Pow((100 + contrast) / 100, 2);

        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                Color pixelColor = image[x, y];                                                                 // Get the pixel color at coordinate (x,y).

                // Apply contrast.
                double red = ((((pixelColor.R / 255.0) - 0.5) * contrast) + 0.5) * 255;
                double green = ((((pixelColor.G / 255.0) - 0.5) * contrast) + 0.5) * 255;
                double blue = ((((pixelColor.B / 255.0) - 0.5) * contrast) + 0.5) * 255;

                red = Clamp(red, 0, 255);
                green = Clamp(green, 0, 255);
                blue = Clamp(blue, 0, 255);

                image[x, y] = Color.FromArgb((byte)red, (byte)green, (byte)blue);                               // Set the new pixel color at coordinate (x,y).
            }
        }
    }

    // Clamps a value between a minimum and maximum value.
    private double Clamp(double value, double min, double max)
    {
        if (value < min)
            value = min;
        if (value > max)
            value = max;

        return value;
    }

    
}