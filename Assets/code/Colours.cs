using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public enum ColourEnum
{
    red, blue, green, orange
}
[System.Serializable]
public class ColourHandler
{
    public int red, blue, green, orange = 0;
    private static Dictionary<ColourEnum, int> colourCounts = new Dictionary<ColourEnum, int>();
    
    public static void Reset()
    {
        colourCounts = new Dictionary<ColourEnum, int>();
    }

    public static void AddCount(ColourEnum colour, int amount)
    {
        if (!colourCounts.ContainsKey(colour))
            colourCounts.Add(colour, 0);
        colourCounts[colour] = Math.Max(0, colourCounts[colour] + amount);
    }

    public static int CountColour(ColourEnum colour)
    {
        try
        {
            return colourCounts[colour];
        } catch (KeyNotFoundException)
        {
            return 0;
        }
    }

    public bool CheckAllColours()
    {
        foreach (FieldInfo fieldInfo in this.GetType().GetFields().Where(FieldInfo => FieldInfo.IsPublic))
        {
            int maxValue = (int)fieldInfo.GetValue(this);
            if (maxValue <= 0)
                continue;
            ColourEnum colourEnum = (ColourEnum) Enum.Parse(typeof(ColourEnum), fieldInfo.Name);
            try
            {
                if (colourCounts[colourEnum] < maxValue)
                    return false;
            } catch (KeyNotFoundException)
            {
                return false;
            }
        }
        return true;
    }
}
