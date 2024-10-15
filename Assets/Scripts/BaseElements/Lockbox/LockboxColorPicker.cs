using System.Collections.Generic;
using System.Linq;
using System;

public class LockboxColorPicker
{
    private Random _random = new Random();

    public List<BaseColor> GetColors(Dictionary<BaseColor, int> lockboxesPerColor, int maxCount)
    {
        List<BaseColor> colors = lockboxesPerColor.Keys.ToList();
        colors = colors.OrderBy(c => _random.Next()).ToList();
        return colors.Take(maxCount).ToList();

    }

    public BaseColor GetSingleColor(Dictionary<BaseColor, int> lockboxesPerColor)
    {
        return lockboxesPerColor.Keys.ElementAt(_random.Next(lockboxesPerColor.Count));
    }
}