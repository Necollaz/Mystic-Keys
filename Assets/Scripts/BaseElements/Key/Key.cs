using UnityEngine;

[RequireComponent(typeof(KeyAnimator))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private Renderer _renderer;
    private KeyAnimator _animator;
    private ApplyColorService _applyColorService;

    public BaseColor Color { get; private set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void Initialize(BaseColor color)
    {
        Color = color;

        _applyColorService.Apply(_renderer, color);
    }

    public BaseColor GetColor()
    {
        return Color;
    }
}