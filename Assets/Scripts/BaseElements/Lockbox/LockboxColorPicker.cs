using System.Collections.Generic;
using System.Linq;
using System;

public class LockboxColorPicker
{
    private Random _random = new Random();

    public List<BaseColor> GetColors(Dictionary<BaseColor, int> lockboxesPerColor, int totalAvailable)
    {
        List<BaseColor> lockboxColors = new List<BaseColor>();

        foreach (var pair in lockboxesPerColor)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                lockboxColors.Add(pair.Key);
            }
        }

        if (lockboxColors.Count == 0)
        {
            return lockboxColors;
        }

        lockboxColors = lockboxColors.OrderBy(x => _random.Next()).ToList();

        if (lockboxColors.Count > totalAvailable)
        {
            lockboxColors = lockboxColors.Take(totalAvailable).ToList();
        }

        return lockboxColors;
    }

    public BaseColor GetSingleColor(Dictionary<BaseColor, int> lockboxesPerColor)
    {
        List<BaseColor> сolors = new List<BaseColor>();
        int index = _random.Next(сolors.Count);

        foreach (var pair in lockboxesPerColor)
        {
            if (pair.Value > 0)
            {
                сolors.Add(pair.Key);
            }
        }

        return сolors[index];
    }
}