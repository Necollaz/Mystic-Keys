using System.Collections.Generic;
using System.Linq;

public class LockboxColorPicker
{
    public List<BaseColor> GetColors(KeyLayer keyLayer, int layerIndex,ColorKeyTracker keyTracker, HashSet<BaseColor> processedColors)
    {
        Dictionary<BaseColor, int> layerKeys = keyLayer.GetKeysForLayer(layerIndex);
        List<BaseColor> layerColors = layerKeys.Keys.ToList();

        if (layerColors.Count == 0)
        {
            return new List<BaseColor>();
        }

        List<BaseColor> selectableColors = layerColors
            .Where(color => !processedColors.Contains(color) && keyTracker.GetTotalKeysForColor(color) > 0)
            .ToList();

        return selectableColors;
    }

    public BaseColor? GetSingleColorForLayer(KeyLayer keyLayer, int layerIndex, ColorKeyTracker keyTracker, HashSet<BaseColor> processedColors)
    {
        Dictionary<BaseColor, int> layerKeys = keyLayer.GetKeysForLayer(layerIndex);
        List<BaseColor> layerColors = layerKeys.Keys.ToList();

        List<BaseColor> selectableColors = layerColors
            .Where(color => !processedColors.Contains(color) && keyTracker.GetTotalKeysForColor(color) > 0)
            .ToList();

        if (selectableColors.Count > 0)
        {
            return selectableColors[0];
        }

        return null;
    }

    public BaseColor? GetNextAvailableColor(KeyLayer keyLayer, ColorKeyTracker colorKeyTracker, HashSet<BaseColor> processedColors)
    {
        for (int layer = 1; layer <= keyLayer.Layers.Length; layer++)
        {
            List<BaseColor> availableColors = GetColors(keyLayer, layer, colorKeyTracker, processedColors);

            foreach (BaseColor color in availableColors)
            {
                if (!processedColors.Contains(color) && colorKeyTracker.GetTotalKeysForColor(color) > 0)
                {
                    processedColors.Add(color);
                    return color;
                }
            }
        }

        return null;
    }
}

//private readonly KeyLayer _keyLayer;
//private readonly ColorKeyTracker _colorKeyTracker;
//private readonly LockboxCalculator _lockboxCalculator;

//public LockboxColorPicker(KeyLayer keyLayer, ColorKeyTracker colorKeyTracker, LockboxCalculator lockboxCalculator)
//{
//    _keyLayer = keyLayer;
//    _colorKeyTracker = colorKeyTracker;
//    _lockboxCalculator = lockboxCalculator;
//}

//public List<BaseColor> GetColors(int maxCount)
//{
//    Debug.Log($"LockboxColorPicker GetColors called with maxCount={maxCount}.");

//    if (maxCount <= 0)
//    {
//        Debug.Log("Максимальное количество сундуков должно быть положительным числом.");
//    }

//    Dictionary<BaseColor, int> availableKeyCounts = _colorKeyTracker.GetPerColor();
//    Dictionary<BaseColor, int> lockboxesPerColor = _lockboxCalculator.CalculatePerColor(availableKeyCounts);
//    List<BaseColor> selectedColors = new List<BaseColor>();
//    int layerIndex = _keyLayer.CurrentLayerIndex;

//    while (selectedColors.Count < maxCount && layerIndex < _keyLayer.Layers.Length)
//    {
//        List<BaseColor> layerColors = GetDistinctColorsFromLayer(layerIndex);

//        List<BaseColor> availableColors = layerColors
//            .Where(color => lockboxesPerColor.ContainsKey(color) && lockboxesPerColor[color] > 0)
//            .OrderBy(color => Random.Range(0, int.MaxValue))
//            .ToList();

//        foreach (BaseColor color in availableColors)
//        {
//            if (selectedColors.Count >= maxCount)
//            {
//                break;
//            }

//            selectedColors.Add(color);
//            lockboxesPerColor[color]--;

//            _colorKeyTracker.Reserve(color, _lockboxCalculator.KeysPerLockbox);
//        }

//        layerIndex++;
//    }

//    return selectedColors;
//}

//public BaseColor? GetSingleColor()
//{
//    Debug.Log("LockboxColorPicker GetSingleColor called.");

//    Dictionary<BaseColor, int> availableKeyCounts = _colorKeyTracker.GetPerColor();
//    Dictionary<BaseColor, int> lockboxesPerColor = _lockboxCalculator.CalculatePerColor(availableKeyCounts);
//    int layerIndex = _keyLayer.CurrentLayerIndex;

//    while (layerIndex < _keyLayer.Layers.Length)
//    {
//        List<BaseColor> layerColors = GetDistinctColorsFromLayer(layerIndex)
//            .Where(color => lockboxesPerColor.ContainsKey(color) && lockboxesPerColor[color] > 0)
//            .Distinct()
//            .ToList();

//        if (layerColors.Any())
//        {
//            BaseColor selectedColor = layerColors[Random.Range(0, layerColors.Count)];

//            lockboxesPerColor[selectedColor]--;

//            _colorKeyTracker.Reserve(selectedColor, _lockboxCalculator.KeysPerLockbox);

//            return selectedColor;
//        }

//        layerIndex++;
//    }

//    return null;
//}

//private List<BaseColor> GetDistinctColorsFromLayer(int layerIndex)
//{
//    if (_keyLayer == null)
//    {
//        Debug.Log("KeyLayer не установлен.");
//    }

//    HashSet<Key> activeKeys = _keyLayer.GetActiveKeys(layerIndex);

//    if (activeKeys == null || activeKeys.Count == 0)
//    {
//        return new List<BaseColor>();
//    }

//    return activeKeys.Select(key => key.Color).Distinct().ToList();
//}