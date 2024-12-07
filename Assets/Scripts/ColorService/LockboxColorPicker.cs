using System.Collections.Generic;
using System.Linq;
using LayersAndGroup;

namespace ColorService
{
    public class LockboxColorPicker
    {
        public BaseColors? GetSingleForLayer(KeyLayer keyLayer, ColorKeyTracker colorKeyTracker, int requiredKeys)
        {
            for (int layer = 0; layer < keyLayer.Layers.Length; layer++)
            {
                List<BaseColors> availableColors = FetchSelectable(keyLayer, layer, colorKeyTracker, requiredKeys);

                foreach (var color in availableColors)
                {
                    if (colorKeyTracker.TrySelectColor(color, requiredKeys))
                    {
                        return color;
                    }
                }
            }

            return null;
        }

        private List<BaseColors> FetchSelectable(KeyLayer keyLayer, int layerIndex, ColorKeyTracker keyTracker, int requiredKeys)
        {
            Dictionary<BaseColors, int> layerKeys = keyLayer.GetKeysForLayer(layerIndex);
            List<BaseColors> layerColors = layerKeys.Keys.ToList();

            if (layerColors.Count == 0)
            {
                return new List<BaseColors>();
            }

            List<BaseColors> selectableColors = layerColors
                .Where(color => keyTracker.GetTotalForColor(color) >= requiredKeys)
                .ToList();

            return selectableColors;
        }
    }
}