using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

// Helpful links:
// https://softwarebydefault.com/2013/05/19/image-erosion-dilation/
// https://en.wikipedia.org/wiki/Median_filter
// REMOVE THIS!!!!

public partial class ImageProcessing : Form
{
    // Static instance of the class, allowing access to its variables from other classes.
    public static ImageProcessing imageProcessing;

    // Input and output image.
    public Bitmap inputImage;
    public Bitmap outputImage;

    public Color[,] image;                                                 // Create array to speed-up operations (Bitmap functions are very slow)
    public Color[,] originalImage;                                         // The colours of the original unchanged input image file.

    public ImageProcessing()
    {
        InitializeComponent();

        imageProcessing = this;
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



    #endregion

    private void Detect_Click(object sender, EventArgs e)
    {
        // Check if there is an input image.
        if (inputImage == null)
        {
            MessageBox.Show("There is no input image!");
            return;
        }

        PrepareImageProcessing();
        TwoPassConnectedComponentLabeling();
        GetCardTypes(detectedObjects);
    }

    private void Dilation_Click(object sender, EventArgs e)
    {
        // Check if there is an input image.
        if (inputImage == null)
        {
            MessageBox.Show("There is no input image!");
            return;
        }

        PrepareImageProcessing();
        Dilation();

    }
}
