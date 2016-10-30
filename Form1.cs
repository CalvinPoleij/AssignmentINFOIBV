using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Helpful links:
// https://softwarebydefault.com/2013/05/19/image-erosion-dilation/
// https://en.wikipedia.org/wiki/Median_filter
// REMOVE THIS!!!!

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap inputImage;
        private Bitmap outputImage;

        private Color[,] image;                                                 // Create array to speed-up operations (Bitmap functions are very slow)
        private Color[,] originalImage;                                         // The colours of the original unchanged input image file.

        public INFOIBV()
        {
            InitializeComponent();
        }
        
        #region Buttons

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)             // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                imageFileName.Text = file;                                  // Show file name
                if (inputImage != null) inputImage.Dispose();               // Reset image
                inputImage = new Bitmap(file);                              // Create new Bitmap from file
                if (inputImage.Height <= 0 || inputImage.Width <= 0 ||
                    inputImage.Height > 512 || inputImage.Width > 512) // Dimension check
                    MessageBox.Show("Error: Image dimensions have to be > 0 and <= 512!");
                else
                {
                    pictureBox1.Image = inputImage;                 // Display input image

                    // Save the original loaded inputImage in a separate array.
                    originalImage = new Color[inputImage.Width, inputImage.Height];

                    // Copy the input Bitmap to an array of colors.
                    for (int x = 0; x < inputImage.Width; x++)
                        for (int y = 0; y < inputImage.Height; y++)
                            originalImage[x, y] = inputImage.GetPixel(x, y);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (outputImage == null)
            {
                MessageBox.Show("There is no output image to save!");
                return;                                // Get out if no output image
            }

            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                outputImage.Save(saveImageDialog.FileName);                 // Save the output image
        }

        // Switches the input image and the output image.
        private void switchButton_Click(object sender, EventArgs e)
        {
            if (outputImage == null)
            {
                MessageBox.Show("You need two images to switch them!");
                return;
            }

            // Save the colors of the current inputImage in a temporary array.
            Color[,] tempImage = new Color[inputImage.Width, inputImage.Height];
            for (int x = 0; x < inputImage.Width; x++)
                for (int y = 0; y < inputImage.Height; y++)
                    tempImage[x, y] = inputImage.GetPixel(x, y);

            // Reset the input image
            inputImage.Dispose();

            // Create a new bitmap
            inputImage = new Bitmap(outputImage.Width, outputImage.Height);
            outputImage = new Bitmap(inputImage.Width, inputImage.Height);

            // Copy the output array of colors to the input Bitmap, and the temp array to the output Bitmap.
            for (int x = 0; x < inputImage.Width; x++)
                for (int y = 0; y < inputImage.Height; y++)
                {
                    inputImage.SetPixel(x, y, image[x, y]);
                    outputImage.SetPixel(x, y, tempImage[x, y]);
                    image[x, y] = outputImage.GetPixel(x, y);
                }

            // Show the correct images.
            pictureBox1.Image = inputImage;
            pictureBox2.Image = outputImage;
        }

        // Reverts the input image to the original image file.
        private void revertOriginalButton_Click(object sender, EventArgs e)
        {
            // Reset the input image
            if (inputImage == null)
            {
                MessageBox.Show("There is no original image!");
                return;
            }

            inputImage.Dispose();

            // Create a new bitmap
            inputImage = new Bitmap(originalImage.GetLength(0), originalImage.GetLength(1));

            // Copy the output array of colors to the input Bitmap.
            for (int x = 0; x < inputImage.Width; x++)
                for (int y = 0; y < inputImage.Height; y++)
                    inputImage.SetPixel(x, y, originalImage[x, y]);

            pictureBox1.Image = inputImage;
        }

        // Clears both the input image and the output image.
        private void clearInputButton_Click(object sender, EventArgs e)
        {
            if (inputImage == null)
            {
                MessageBox.Show("There is no image to clear!");
                return;
            }
            inputImage = null;
            outputImage = null;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }

        // Clears the output image.
        private void clearOutputButton_Click(object sender, EventArgs e)
        {
            if (outputImage == null)
            {
                MessageBox.Show("There is no Output image to clear!");
                return;
            }
            outputImage = null;
            pictureBox2.Image = null;
        }

        // Applies a negative effect to the input image.
        private void negativeButton_Click(object sender, EventArgs e)
        {
            // Check if there is an input image.
            if (inputImage == null)
            {
                MessageBox.Show("There is no input image!");
                return;
            }

            PrepareImageProcessing();

            // Apply image processing effect.
            ApplyNegative();

            ShowOutputImage();
        }

        private void contrastButton_Click(object sender, EventArgs e)
        {
            // Check if there is an input image.
            if (inputImage == null)
            {
                MessageBox.Show("There is no input image!");
                return;
            }

            PrepareImageProcessing();

            // Apply image processing effect.
            ApplyContrast(contrastTracker.Value);

            ShowOutputImage();
        }

        // Applies a grayscale effect to the input image.
        private void grayScaleButton_Click(object sender, EventArgs e)
        {
            // Check if there is an input image.
            if (inputImage == null)
            {
                MessageBox.Show("There is no input image!");
                return;
            }

            PrepareImageProcessing();

            // Apply image processing effect.
            ApplyGrayScale();

            ShowOutputImage();
        }

        // Applies thresholding to the input image, resulting in a binary image.
        private void applyThresholdButton_Click(object sender, EventArgs e)
        {
            // Check if there is an input image.
            if (inputImage == null)
            {
                MessageBox.Show("There is no input image!");
                return;
            }

            PrepareImageProcessing();

            // Apply image processing effect.
            ApplyThreshold();
            ShowOutputImage();
        }

        #endregion

        #region Image Processing Effects

        // Creates a negative image. (included as an example in the template)
        private void ApplyNegative()
        {
            for (int x = 0; x < inputImage.Size.Width; x++)
            {
                for (int y = 0; y < inputImage.Size.Height; y++)
                {
                    Color pixelColor = image[x, y];                                                                     // Get the pixel color at coordinate (x,y).
                    Color updatedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);    // Negative color.
                    image[x, y] = updatedColor;                                                                         // Set the new pixel color at coordinate (x,y).
                    progressBar.PerformStep();                                                                          // Increment progress bar.
                }
            }
        }

        // Creates a grayscaled image.
        private void ApplyGrayScale()
        {
            for (int x = 0; x < inputImage.Size.Width; x++)
            {
                for (int y = 0; y < inputImage.Size.Height; y++)
                {
                    Color pixelColor = image[x, y];                                                                 // Get the pixel color at coordinate (x,y).

                    // Apply greyscale.
                    int grayScale = (int)(pixelColor.R * 0.299f + pixelColor.G * 0.587f + pixelColor.B * 0.114f);   // Calculates the correct grayscale value using the luma component.
                    Color updatedColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);             // The new color, now with grayscale applied.

                    image[x, y] = updatedColor;                                                                     // Set the new pixel color at coordinate (x,y).
                    progressBar.PerformStep();                                                                      // Increment progress bar.
                }
            }
        }

        // Applies thresholding to an image, given a threshold value. Returns a binary image.
        private void ApplyThreshold()
        {
            // Convert image to black and white based on average brightness
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    // Set this pixel to black or white based on threshold
                    if (image[x, y].GetBrightness() >= (double)thresholdTracker.Value / thresholdTracker.Maximum)
                        image[x, y] = Color.White;
                    else
                        image[x, y] = Color.Black;
                }
            }
        }

        // Applies contrasting to an image, given a contrast value.
        private void ApplyContrast(double contrast)
        {
            contrast = Clamp(contrast, -100, 100);
            contrast = Math.Pow((100 + contrast) / 100, 2);

            for (int x = 0; x < inputImage.Size.Width; x++)
            {
                for (int y = 0; y < inputImage.Size.Height; y++)
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
                    progressBar.PerformStep();                                                                      // Increment progress bar.
                }
            }
        }

        // Applies opening by reconstruction to an image.
        private void ApplyOpenReconstruction()
        {

        }

        #endregion

        #region Other Methods

        // Does everything necessary to process the input image.
        private void PrepareImageProcessing()
        {
            // Reset the output image.
            if (outputImage != null)
                outputImage.Dispose();

            SetupProgressBar();
            PrepareColorArray();
        }

        // Resets the progress bar.
        private void SetupProgressBar()
        {
            progressBar.Minimum = 1;
            progressBar.Maximum = inputImage.Width * inputImage.Height;
            progressBar.Step = 1;
        }

        // Fills an array of colors with all values from the input image.
        private void PrepareColorArray()
        {
            image = new Color[inputImage.Width, inputImage.Height];

            // Copy the input Bitmap to an array of colors.
            for (int x = 0; x < inputImage.Width; x++)
                for (int y = 0; y < inputImage.Height; y++)
                    image[x, y] = inputImage.GetPixel(x, y);
        }

        // Shows the output Image in the right picture box.
        private void ShowOutputImage()
        {
            // Create new output image.
            outputImage = new Bitmap(inputImage.Width, inputImage.Height);

            // Copy the updated array of colors to the output Bitmap.
            for (int x = 0; x < inputImage.Width; x++)
                for (int y = 0; y < inputImage.Height; y++)
                    outputImage.SetPixel(x, y, image[x, y]);

            pictureBox2.Image = outputImage;                                // Display output image
            progressBar.Value = 1;                                    // Hide progress bar
        }

        private double Clamp(double value, double min, double max)
        {
            if (value < min)
                value = min;
            if (value > max)
                value = max;

            return value;
        }

        #endregion
    }
}
