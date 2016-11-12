using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DetectedCard
{
    public int id;
    public List<DetectedObject> detectedObjects;
    public int waarde;
    public string type;

    private void value()
    {
        waarde = detectedObjects.Count() - 4;
    }
    
    // Determine the Card Type for an object.
    public string CardType(DetectedCard Card)
    {
        DetectedObject o = Card.detectedObjects[0];

        double compactness = o.Compactness;         //mss vervang dood boundedbox ratio?
        int m1 = 0;     // Margin 1
        int m2 = 0;     // Margin 2
        int m3 = 0;     // Margin 3
        int m4 = 0;     // Margin 4

        string type = "";

        if (compactness > 0 && compactness <= m1)   //hier moeten de verhoudingen van harten, ruiten, schoppen en klaveren, dit zijn nu nog examples
        {
            type = "Harten";
        }
        else if (compactness > m1 && compactness <= m2)
        {
            type = "Ruiten";
        }
        else if (compactness > m2 && compactness <= m3)
        {
            type = "Klaveren";
        }
        else if (compactness > m3 && compactness <= m4)
        {
            type = "Schoppen";
        }

        return type;
    }

    
}

