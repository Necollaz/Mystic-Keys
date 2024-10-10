using UnityEngine;

[RequireComponent(typeof(KeyAnimator))]
[RequireComponent(typeof(Collider))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private Renderer _renderer;
    private ApplyColorService _applyColorService;
    public Collider Collider { get; private set; }

    public BaseColor Color { get; private set; }
    public int LayerIndex { get; set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        Collider = GetComponent<Collider>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void Initialize(BaseColor color)
    {
        Color = color;

        _applyColorService.Apply(_renderer, color);
    }
}