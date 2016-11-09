using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

partial class ImageProcessing
{
    List<DetectedObject> detectedObjects = new List<DetectedObject>();

    // Based on the Two-pass Connected-component labeling algorithm.
    // Labels all the objects in a binary image (which means the image consists only of two colours).
    private void TwoPassConnectedComponentLabeling()
    {
        // Clear previously found list of detectedObjects.
        detectedObjects.Clear();

        // Initialize a label value per pixel in the image. This value is 0 by default, which is the background label.
        int[,] labelValues = new int[inputImage.Width, inputImage.Height];

        // Keep track of all unique labels in a Dictionary.
        Dictionary<int, Label> labels = new Dictionary<int, Label>();

        // The current label ID (which is also equal to the total number of labels).
        int currentLabel = 1;

        // First pass: Scan the whole image, detect non-background pixels and label them.
        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                // Only check the pixels that are not part of the background
                if (image[x, y] != backgroundColor)
                {
                    // List of all neighbouring pixels of the current pixel, whose labelValue is not zero.
                    List<int> neighbours = new List<int>();

                    // Find the qualified neighbouring pixels (if their index is in range).
                    for (int i = x - 1; i < x + 3 && i < inputImage.Width - 1; i++)
                        for (int j = y - 1; j < y + 3 && j < inputImage.Height - 1; j++)
                            if (i >= 0 && j >= 0 && labelValues[i, j] != 0)
                                neighbours.Add(labelValues[i, j]);

                    // If no neighbours were found, create a new label, and increment the currentLabel variable.
                    if (neighbours.Count == 0)
                    {
                        labels.Add(currentLabel, new Label(currentLabel));
                        labelValues[x, y] = currentLabel;
                        currentLabel++;
                    }
                    else
                    {
                        // Set the current pixel label to the smallest label within the neighbours list.
                        int smallestLabel = neighbours.Min();
                        labelValues[x, y] = smallestLabel;

                        // Check the neighbouring pixels and merge equivalent labels.
                        foreach (int label in neighbours)
                            if (labels[smallestLabel].FindRoot().id != labels[label].FindRoot().id)
                                labels[label].Union(labels[smallestLabel]);
                    }
                }
            }
        }

        // Second pass: Group all pixels with the same root label ID together to form a DetectedObject.
        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                if (labelValues[x, y] != 0)
                {
                    // Find the root label id of the current pixel.
                    int labelID = labels[labelValues[x, y]].FindRoot().id;

                    // Check if a DetectedObject exists with an ID matching the found labelID.
                    DetectedObject detectedObject = detectedObjects.Find(o => o.id == labelID);

                    if (detectedObject == null)
                    {
                        detectedObject = new DetectedObject(labelID);
                        detectedObjects.Add(detectedObject);
                    }

                    // Assign pixel to the found or newly created DetectedObject.
                    detectedObject.AddPixel(x, y);
                }
            }
        }

        // Color an object red.
        detectedObjects[4].ColorObject(Color.Red);

        // Debug line that shows how many objects were detected.
        MessageBox.Show(detectedObjects.Count.ToString() + " objects have been detected.");
    }
}
