using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockboxColorPicker
{
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

        if (lockboxColors.Count > totalAvailable)
        {
            lockboxColors = lockboxColors.OrderBy(color => Random.value).Take(totalAvailable).ToList();
        }

        return lockboxColors;
    }
}