using UnityEngine;

public class ApplyColorService
{
    public ColorMaterialPair[] ColorMaterials;

    public void Apply(Renderer renderer, BaseColor color)
    {
        foreach (var pair in ColorMaterials)
        {
            if (pair.color == color)
            {
                renderer.material = pair.material;
                return;
            }
        }
    }
}