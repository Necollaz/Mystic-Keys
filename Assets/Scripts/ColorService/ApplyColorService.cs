using UnityEngine;

namespace ColorService
{
    public class ApplyColorService
    {
        public ColorMaterialPair[] ColorMaterials;

        public void Apply(Renderer renderer, BaseColors color)
        {
            foreach (ColorMaterialPair pair in ColorMaterials)
            {
                if (pair.color == color)
                {
                    renderer.material = pair.material;
                    return;
                }
            }
        }
    }
}