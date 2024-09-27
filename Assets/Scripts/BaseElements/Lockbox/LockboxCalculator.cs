using System.Collections.Generic;

public class LockboxCalculator
{
    private int _value = 3;

    public Dictionary<BaseColor, int> CalculatePerColor(List<ColorKeyCount> colorKeyCounts)
    {
        Dictionary<BaseColor, int> lockboxesPerColor = new Dictionary<BaseColor, int>();

        foreach (var colorKeyCount in colorKeyCounts)
        {
            int lockboxesNeeded = colorKeyCount.KeyCount / _value;
            lockboxesPerColor[colorKeyCount.Color] = lockboxesNeeded;
        }

        return lockboxesPerColor;
    }
}