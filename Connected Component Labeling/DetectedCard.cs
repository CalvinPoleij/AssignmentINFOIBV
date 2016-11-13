using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DetectedCard
{
    public int id;
    public List<DetectedObject> detectedObjects = new List<DetectedObject>();
    

    public int CardValue(List<DetectedObject> detectedObjects)
    {
        int waarde = detectedObjects.Count();
        return waarde;  
    }

    public string CardType(List<DetectedObject> detectedObjects)
    {
        string type = "";
        
        return type;
    }

}
