using System;
using UnityEngine;

[RequireComponent(typeof(KeyAnimator))]
[RequireComponent(typeof(Collider))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;
    [SerializeField] private ParticleSystem _removeKey;

    private Renderer _renderer;
    private KeyAnimator _animator;
    private ApplyColorService _applyColorService;

    public BaseColor Color { get; private set; }
    public int LayerIndex { get; set; }

    public event Action<Key> KeyCollected;

    private void Awake()
    {
        _animator = GetComponent<KeyAnimator>();
        _renderer = GetComponentInChildren<Renderer>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void UseActive()
    {
        _animator.Turn(true);
        _removeKey.Play();
        _animator.Turn(false);
        gameObject.SetActive(false);

        KeyCollected?.Invoke(this);
    }

    public void UseInactive()
    {
        _animator.TryTurn(true);
    }

    public void Initialize(BaseColor color)
    {
        Color = color;

        _applyColorService.Apply(_renderer, color);
    }
}