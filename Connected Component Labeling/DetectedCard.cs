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

    public int CardValue
    {
        get { return cardSymbols.Count; }
    }

    public void ColorCard()
    {
        int greyValue = 150 / ImageProcessing.imageProcessing.detectedCards.Count * id;
        Color color = Color.FromArgb(50 + greyValue, 50 + greyValue, 50 + greyValue);

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

    public static Color Combine(Color a, Color b, float amount = 0.5f)
    {
        return Color.FromArgb((int)(a.R * amount + b.R * amount), (int)(a.G * amount + b.G * amount), (int)(a.B * amount + b.B * amount));
    }
}
