using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(KeyAnimator))]
[RequireComponent(typeof(Collider))]
public class Key : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;
    [SerializeField] private Sprite _keySprite;

    private Renderer _renderer;
    private Collider _collider;
    private KeyAnimator _animator;
    private ApplyColorService _applyColorService;
    private bool _isPickedUp = false;

    public BaseColor Color { get; private set; }
    public int LayerIndex { get; set; }
    public int GroupIndex { get; set; }

    public event Action<Key> Collected;

    private void Awake()
    {
        _animator = GetComponent<KeyAnimator>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void Initialize(BaseColor color)
    {
        Color = color;
        _applyColorService.Apply(_renderer, color);
    }

    public Sprite GetSprite()
    {
        return _keySprite;
    }

    public void UseActive()
    {
        if(_isPickedUp == false)
        {
            _isPickedUp = true;

            if(_collider != null)
            {
                _collider.enabled = false;
            }

            _animator.PlayAnimation();
            Collected?.Invoke(this);
        }
    }

    public void UseInactive()
    {
        StartCoroutine(UseInactiveCoroutine());
    }

    private IEnumerator UseInactiveCoroutine()
    {
        yield return StartCoroutine(_animator.TryTurn());
    }
}