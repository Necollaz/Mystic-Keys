using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private Renderer _renderer;
    private BaseColor _color;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer == null)
        {
            Debug.LogError("Renderer не найден в потомках объекта Key");
        }

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
                Debug.Log($"Цвет ключа установлен на {pair.color}");

                return;
            }
        }
        Debug.LogWarning($"Материал для цвета {_color} не найден!");

    }
}