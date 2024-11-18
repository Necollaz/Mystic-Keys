using System.Collections.Generic;

public class LockboxCalculator
{
    public int KeysPerLockbox { get; private set; } = 3;

    public Dictionary<BaseColor, int> CalculatePerColor(Dictionary<BaseColor, int> colorKeyCounts)
    {
        Dictionary<BaseColor, int> lockboxesPerColor = new Dictionary<BaseColor, int>();

        foreach (KeyValuePair<BaseColor, int> colorKeyCount in colorKeyCounts)
        {
            int colorCount = colorKeyCount.Value;
            int lockboxesNeeded = colorCount / KeysPerLockbox;

            if (lockboxesNeeded > 0)
            {
                lockboxesPerColor[colorKeyCount.Key] = lockboxesNeeded;
            }
        }

        return lockboxesPerColor;
    }
}