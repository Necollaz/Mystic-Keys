using System.Collections.Generic;

public class LockboxCalculator
{
    private int _value = 3;

    public Dictionary<BaseColor, int> CalculatePerColor(Dictionary<BaseColor, int> colorKeyCounts)
    {
        Dictionary<BaseColor, int> lockboxesPerColor = new Dictionary<BaseColor, int>();

        foreach (var colorKeyCount in colorKeyCounts)
        {
            int lockboxesNeeded = colorKeyCount.Value / _value;
            lockboxesPerColor[colorKeyCount.Key] = lockboxesNeeded;
        }

        return lockboxesPerColor;
    }
}