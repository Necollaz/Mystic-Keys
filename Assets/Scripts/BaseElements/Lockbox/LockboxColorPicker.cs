using System.Collections.Generic;
using System.Linq;

public class LockboxColorPicker
{
    public List<BaseColor> GetColors(Dictionary<BaseColor, int> lockboxesPerColor, int maxCount)
    {
        return lockboxesPerColor.Keys.Take(maxCount).ToList();
    }

    public BaseColor GetSingleColor(Dictionary<BaseColor, int> lockboxesPerColor)
    {
        return lockboxesPerColor.Keys.FirstOrDefault();
    }
}