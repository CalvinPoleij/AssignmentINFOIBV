using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

partial class ImageProcessing
{
    public List<DetectedObject> detectedObjects = new List<DetectedObject>();

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

                    // Assign pixel to the found or newly created DetectedObject. Also, check if the pixel is a perimeter pixel.
                    detectedObject.AddPixel(x, y, CheckPerimeterPixel(x, y));
                }
            }
        }

        // Color an object red.
        if (detectedObjects.Count > 0)
        {
            detectedObjects[0].ColorObject(Color.Red);
            detectedObjects[0].ColorPerimeter(Color.Green);
            detectedObjects[0].ColorBoundingBox(Color.Blue);
            detectedObjects[0].ConvexHull();
        }

        // Debug line that shows how many objects were detected.
        MessageBox.Show(detectedObjects.Count.ToString() + " objects have been detected.");
    }

    // Check for a given point if it is a perimeter pixel (That is, if any of its neighbouring pixel is a background pixel).
    public bool CheckPerimeterPixel(int x, int y, bool checkDiagonals = false)
    {
        for (int i = x - 1; i < x + 2 && i < inputImage.Width; i++)
            for (int j = y - 1; j < y + 2 && j < inputImage.Height; j++)
            {
                if (!checkDiagonals && (j - y == i - x || j - y == -(i - x) || -(j - y) == i - x))
                    continue;

                if (i >= 0 && j >= 0 && image[i, j] == backgroundColor)
                    return true;
            }
        return false;
    }

    private List<string> GetCardTypes(List<DetectedObject> a)   //bepaal voor elk object het type,, moet voor elke kaart zijn dus er moet eigk nog een list gemaakt worden 
    {                                                           // met voor elke kaart 1 object
        List<string> objecttype = new List<string>();
        for (int i = 0; i < a.Count(); i++)
        {
            objecttype.Add(CardType(a[i]));
        }
        MessageBox.Show(objecttype[1]);
        return objecttype;
    }

    // Determine the Card Type for an object.
    public string CardType(DetectedObject a)
    {
        float v = a.AreaPerimeterRatio;         // Get the Area Perimeter Ratio of the DetectedObject.

        int m1 = 0;     // Margin 1
        int m2 = 0;     // Margin 2
        int m3 = 0;     // Margin 3
        int m4 = 0;     // Margin 4

        string type = "";

        if (v > 0 && v <= m1)   //hier moeten de verhoudingen van harten, ruiten, schoppen en klaveren, dit zijn nu nog examples
        {
            type = "Harten";
        }
        else if (v > m1 && v <= m2)
        {
            type = "Ruiten";
        }
        else if (v > m2 && v <= m3)
        {
            type = "Klaveren";
        }
        else if (v > m3 && v <= m4)
        {
            type = "Schoppen";
        }

        return type;
    }

    public void Dilation()      //Dilation
    {

        foreach (DetectedObject c in detectedObjects)
        {
            foreach (Point p in c.perimeterPixels)//om p alles kleuren in + vorm, dus boven, onder, links, rechts, tenzij al onderdeel van het object
            {
                if (outputImage.GetPixel(p.X, p.Y + 1) == Color.White)  //pixel boven p
                {
                    outputImage.SetPixel(p.X, p.Y + 1, Color.Black);
                    c.perimeterPixels.Add(new Point(p.X, p.Y + 1));    //voeg nieuwe perimeter pixel toe aan lijst
                    c.pixels.Add(new Point(p.X, p.Y + 1));              //update ook de pixel list
                }

                if (outputImage.GetPixel(p.X, p.Y - 1) == Color.White)  //pixel onder p
                {
                    outputImage.SetPixel(p.X, p.Y - 1, Color.Black);
                    c.perimeterPixels.Add(new Point(p.X, p.Y - 1));
                    c.pixels.Add(new Point(p.X, p.Y - 1));
                }

                if (outputImage.GetPixel(p.X - 1, p.Y) == Color.White)   //pixel links p
                {
                    outputImage.SetPixel(p.X - 1, p.Y, Color.Black);
                    c.perimeterPixels.Add(new Point(p.X - 1, p.Y));
                    c.pixels.Add(new Point(p.X - 1, p.Y));
                }

                if (outputImage.GetPixel(p.X + 1, p.Y) == Color.White)  //pixel rechts p
                {
                    outputImage.SetPixel(p.X + 1, p.Y, Color.Black);
                    c.perimeterPixels.Add(new Point(p.X + 1, p.Y));
                    c.pixels.Add(new Point(p.X + 1, p.Y));
                }
                c.perimeterPixels.Remove(p);                            //p is niet langer perimeter pixel, remove

            }
        }
    }

    public void Erosion()       //Erosion
    {
        foreach (DetectedObject c in detectedObjects)
        {
            foreach (Point p in c.perimeterPixels)
            {

            }
        }
    }
}
