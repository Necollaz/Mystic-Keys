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
    public bool IsInteractive { get; private set; }

    public event Action<Key> Collected;

    private void Awake()
    {
        _animator = GetComponent<KeyAnimator>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<Renderer>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
        _animator.CollectedComplete += OnAnimationComplete;
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

    public void SetInteractivity(bool isActive)
    {
        IsInteractive = isActive;
        _collider.enabled = isActive;
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

    private void OnAnimationComplete()
    {
        _animator.CollectedComplete -= OnAnimationComplete;
        gameObject.SetActive(false);
        Collected?.Invoke(this);
    }
}