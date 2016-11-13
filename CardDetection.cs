using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

partial class ImageProcessing
{
    public List<DetectedCard> detectedCards = new List<DetectedCard>();

    public float compactnessThresholdA = 1.3f, compactnessThresholdB = 1.8f;
    public float elongationThreshold = 1.15f;

    // Based on the Two-pass Connected-component labeling algorithm.
    // Labels all the objects in a binary image (which means the image consists only of two colours).
    private void CardDetection()
    {
        // Clear previously found list of detectedCards.
        detectedCards.Clear();

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

        // Second pass: Group all pixels with the same root label ID together to form a DetectedCard.
        for (int x = 0; x < inputImage.Width; x++)
        {
            for (int y = 0; y < inputImage.Height; y++)
            {
                if (labelValues[x, y] != 0)
                {
                    // Find the root label id of the current pixel.
                    int labelID = labels[labelValues[x, y]].FindRoot().id;

                    // Check if a DetectedCard exists with an ID matching the found labelID.
                    DetectedCard detectedCard = detectedCards.Find(o => o.id == labelID);

                    if (detectedCard == null)
                    {
                        detectedCard = new DetectedCard(labelID);
                        detectedCards.Add(detectedCard);
                    }

                    // Assign pixel to the found or newly created DetectedCard. Also, check if the pixel is a perimeter pixel.
                    detectedCard.AddPixel(x, y, CheckPerimeterPixel(x, y));
                }
            }
        }

        // Debug line that shows how many objects were detected.
        MessageBox.Show(detectedCards.Count.ToString() + " cards have been detected.");
    }

    // Compares each detectedObject (not card!) to the detected cards, and determines on which card they belong.
    private void CoupleObjectsWithCards()
    {
        foreach (DetectedObject detectedObject in detectedObjects)
        {
            // Check if any of the neighbours of the first perimeterPixel of the detectedObject belongs to the card.
            // If so, this object is located on that card. Else, move on to the next card.
            Point perimeterPixel = detectedObject.perimeterPixels[0];
            List<Point> neighbours = GetPixelNeighbours(perimeterPixel.X, perimeterPixel.Y);
            bool coupled = false;

            // If the neighbour is located on the card, couple the object and move on.
            foreach (DetectedCard detectedCard in detectedCards)
            {
                foreach (Point pixel in neighbours)
                    if (detectedCard.pixels.Contains(pixel))
                    {
                        detectedCard.cardSymbols.Add(detectedObject);
                        coupled = true;
                        break;
                    }

                if (coupled)
                    break;
            }
        }
    }

    private void DetectCardSymbols()
    {
        foreach (DetectedCard detectedCard in detectedCards)
        {
            DetectedObject symbol = detectedCard.cardSymbols[2];

            // Series of if statements to determine what the symbol is.
            // If below the compactness threshold A, the symbol could be either a Heart of a Diamond.
            // If above the compactness threshold A, the symbol could be either a Club (Klaver) or a Spade (Schoppen).
            // The Clubs are always above the compactness threshold B.

            if (symbol.Compactness < compactnessThresholdA)
            {
                if (symbol.Elongation < elongationThreshold)
                    detectedCard.cardType = DetectedCard.CardType.Hearts;
                else
                    detectedCard.cardType = DetectedCard.CardType.Diamonds;
            }
            else if (symbol.Compactness < compactnessThresholdB)
                detectedCard.cardType = DetectedCard.CardType.Spades;
            else
                detectedCard.cardType = DetectedCard.CardType.Clubs;

            detectedCard.ColorCard();
        }
    }
}