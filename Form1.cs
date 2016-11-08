using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// Helpful links:
// https://softwarebydefault.com/2013/05/19/image-erosion-dilation/
// https://en.wikipedia.org/wiki/Median_filter
// https://en.wikipedia.org/wiki/Connected-component_labeling
// REMOVE THIS!!!!

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap inputImage;
        private Bitmap outputImage;

        private Color[,] image;                                                 // Create array to speed-up operations (Bitmap functions are very slow)
        private Color[,] originalImage;                                         // The colours of the original unchanged input image file.

        // Determine background- and foreground colors of a binary image.
        Color backgroundColor = Color.Black;
        Color foregroundColor = Color.White;

        Dictionary<int, DetectedObject> detectedObjects;

        public INFOIBV()
        {
            InitializeComponent();
        }

        #region Buttons

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)            // Open File Dialog
            {
                string file = openImageDialog.FileName;                     // Get the file name
                imageFileName.Text = file;                                  // Show file name

                if (inputImage != null)
                    inputImage.Dispose();                                   // Reset image

                inputImage = new Bitmap(file);                              // Create new Bitmap from file

                // Dimension check. Resizes the image to fit inside the picture box.
                if (inputImage.Height <= 0 || inputImage.Width <= 0 || inputImage.Height > 512 || inputImage.Width > 512) 
                {
                    float scale = Math.Min(512 / (float)inputImage.Width, 512 / (float)inputImage.Height);
                    inputImage = new Bitmap(inputImage, (int)(inputImage.Width * scale), (int)(inputImage.Height * scale));
                }

                // Display input image
                pictureBox1.Image = inputImage;

                // Save the original loaded inputImage in a separate array.
                originalImage = new Color[inputImage.Width, inputImage.Height];

                // Copy the input Bitmap to an array of colors.
                for (int x = 0; x < inputImage.Width; x++)
                    for (int y = 0; y < inputImage.Height; y++)
                        originalImage[x, y] = inputImage.GetPixel(x, y);
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

        private void componentLabelingButton_Click(object sender, EventArgs e)
        {
            // Check if there is an input image.
            if (inputImage == null)
            {
                MessageBox.Show("There is no input image!");
                return;
            }

            PrepareImageProcessing();

            ApplyThreshold(true);
            TwoPassConnectedComponentLabeling();

            ShowOutputImage();
        }

        #endregion

        #region Image Processing Effects

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

        // Labels all the objects in the image. (image should be binary) (4-connectivity based: Only the north and west neighbours are checked)
        private void TwoPassConnectedComponentLabeling()
        {
            detectedObjects = new Dictionary<int, DetectedObject>();

            Dictionary<int, LinkedList<int>> regionLinks = new Dictionary<int, LinkedList<int>>();
            int currentLabel = 1;

            // Initialize labels
            int[,] labels = new int[inputImage.Width, inputImage.Height];

            // First pass: Detect different components and label them.
            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    // Only check the pixels that are not part of the background
                    if (image[x, y] != backgroundColor)
                    {
                        // List of all qualified neighbours of the current pixel.
                        List<int> neighbours = new List<int>();

                        // Set the neighbouring pixels (if their index is in range).
                        if (x > 0 && y > 0)
                        {
                            neighbours.Add(labels[x - 1, y]);       // West
                            neighbours.Add(labels[x, y - 1]);       // North
                        }

                        // Remove all neighbours with a background label.
                        neighbours.RemoveAll(i => i == 0);

                        // If the resulting smallestLabel is zero (the background label), no neighbours were found.
                        if (neighbours.Count == 0)
                        {
                            // If no neighbours were found, create a new label.
                            regionLinks.Add(currentLabel, new LinkedList<int>());
                            regionLinks[currentLabel].AddFirst(currentLabel);
                            labels[x, y] = currentLabel;
                            currentLabel++;
                        }
                        else
                        {
                            // Set the current pixel label to the smallest label within the neighbours list.
                            int smallestLabel = neighbours.Min();
                            labels[x, y] = smallestLabel;

                            // Check the neighbours and merge equivalent labels.
                            foreach (int label in neighbours)
                            {
                                if (!regionLinks[smallestLabel].Contains(label) && label < regionLinks[smallestLabel].First.Value)
                                    regionLinks[smallestLabel].AddFirst(label);
                            }
                        }
                    }
                }
            }

            // Second pass: Relabel the pixels with the lowest label value, so that pixels of the same region all have the same label.
            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    if (labels[x,y] != 0)
                    {
                        // Relabel the pixel.
                        int label = regionLinks[labels[x, y]].First.Value;
                        labels[x, y] = label;

                        // If a new label is detected, add an object to the detectedObjects.
                        if (!detectedObjects.ContainsKey(label))
                            detectedObjects.Add(label, new DetectedObject());

                        // Assign pixel to its label group.
                        detectedObjects[label].AddPixel(x, y);

                        // Set the pixel color.
                        image[x, y] = Color.Red;
                    }
                }
            }

            
            //Decide type of card (hearts, diamonds, clovers, pikes)
            //mijn test om te kijken wtf je code precies doet :P
            //de int in de dictionairy is dus niet het aantal pixels van het object? ik dacht dat je dat opwhatsapp zei :S
            foreach (KeyValuePair<int, DetectedObject > entry in detectedObjects)
            {
                int i = entry.Key;
                MessageBox.Show(i.ToString());
                
            }

           
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

            // Display output image
            pictureBox2.Image = outputImage;

            // Hide progress bar
            progressBar.Value = 1;
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

        #endregion
    }

    public class DetectedObject
    {
        public List<Point> pixels = new List<Point>();

        public void AddPixel(int x, int y)
        {
            pixels.Add(new Point(x, y));
        }
    }
}
