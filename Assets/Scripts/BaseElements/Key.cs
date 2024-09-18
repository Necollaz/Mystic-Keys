using UnityEngine;

[System.Serializable]
public struct ColorMaterialPair
{
    public BaseColor color;
    public Material material;
}

[RequireComponent(typeof(AnimationController))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private AnimationController _animationController;
    private Renderer _renderer;
    private BaseColor _color;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _animationController = GetComponent<AnimationController>();
    }

    public void Initialize(BaseColor color)
    {
        _color = color;

        ApplyColor();
    }

    public BaseColor GetColor()
    {
        return _color;
    }

    public void Use(Lockbox lockbox)
    {
        lockbox.AddKey(this);
    }

    private void ApplyColor()
    {
        foreach (var pair in _colorMaterials)
        {
            if (pair.color.Equals(_color))
            {
                _renderer.material = pair.material;
                return;
            }
        }
    }
}