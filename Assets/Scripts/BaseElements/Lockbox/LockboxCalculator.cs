using System.Collections.Generic;

public class LockboxCalculator
{
    private int _keysPerLockbox = 3;

    public int KeysPerLockbox => _keysPerLockbox;

    public Dictionary<BaseColor, int> CalculatePerColor(Dictionary<BaseColor, int> colorKeyCounts)
    {
        Dictionary<BaseColor, int> lockboxesPerColor = new Dictionary<BaseColor, int>();

        foreach (var colorKeyCount in colorKeyCounts)
        {
            int colorCount = colorKeyCount.Value;
            int lockboxesNeeded = colorCount / _keysPerLockbox;

            if (lockboxesNeeded > 0)
            {
                lockboxesPerColor[colorKeyCount.Key] = lockboxesNeeded;
            }
        }

        return lockboxesPerColor;
    }
}