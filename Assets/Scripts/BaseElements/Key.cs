using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private Renderer _renderer;
    private ApplyColorService _applyColorService;
    private BaseColor _color;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void Initialize(BaseColor color)
    {
        _color = color;

        _applyColorService.Apply(_renderer, color);
    }

    public BaseColor GetColor()
    {
        return _color;
    }

    public void Use(Lockbox lockbox)
    {
        lockbox.AddKey(this);
    }
}