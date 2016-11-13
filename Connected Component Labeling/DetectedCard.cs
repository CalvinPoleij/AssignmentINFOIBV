using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

public class DetectedCard : DetectedObject
{
    public enum CardType { Undefined, Hearts, Diamonds, Clubs, Spades }
    public CardType cardType = CardType.Undefined;

    public List<DetectedObject> cardSymbols = new List<DetectedObject>();

    public DetectedCard(int id) : base(id)
    {
    }

    // Determines the value of the card by counting the amount of objects on the card.
    public string CardValue
    {
        get
        {
            if (cardSymbols.Count >= 16)
                return "10";
            else if (cardSymbols.Count == 5)
                return "Ace";
            else
                return (cardSymbols.Count - 4).ToString();
        }
    }

    // Give the card a color, and all objects on it as well.
    public void ColorCard(int greyValue)
    {
        greyValue = (int)ImageProcessing.imageProcessing.Clamp(greyValue, 0, 255);

        Color color = Color.FromArgb(greyValue, greyValue, greyValue);

        ColorObject(color);

        switch (cardType)
        {
            case CardType.Hearts:
                color = Combine(color, Color.Red);
                break;
            case CardType.Diamonds:
                color = Combine(color, Color.Blue);
                break;
            case CardType.Clubs:
                color = Combine(color, Color.Green);
                break;
            case CardType.Spades:
                color = Combine(color, Color.Yellow);
                break;
        }

        foreach (DetectedObject symbol in cardSymbols)
            symbol.ColorObject(color);
    }

    // Combines two colors.
    public static Color Combine(Color a, Color b, float amount = 0.5f)
    {
        return Color.FromArgb((int)(a.R * amount + b.R * amount), (int)(a.G * amount + b.G * amount), (int)(a.B * amount + b.B * amount));
    }
}
