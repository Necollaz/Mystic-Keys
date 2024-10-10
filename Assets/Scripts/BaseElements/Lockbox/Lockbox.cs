using System;
using UnityEngine;

[RequireComponent(typeof(LockboxAnimator))]
public class Lockbox : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;
    [SerializeField] private int _requiredKeys = 3;

    private int _currentKeys = 0;
    private Renderer _renderer;
    private LockboxAnimator _animator;
    private ApplyColorService _applyColorService;

    public BaseColor Color { get; private set; }
    public int CurrentKeyCount => _currentKeys;

    public event Action<Lockbox, int> KeyAdded;
    public event Action<Lockbox> Filled;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _animator = GetComponent<LockboxAnimator>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
    }

    public void Initialize(BaseColor color)
    {
        Color = color;
        _currentKeys = 0;

        _applyColorService.Apply(_renderer, Color);
        _animator.Appearance();
        StartCoroutine(_animator.Open());
    }

    public void AddKey()
    {
        if (_currentKeys < _requiredKeys)
        {
            _currentKeys++;
            KeyAdded?.Invoke(this, _currentKeys);

            if (_currentKeys >= _requiredKeys)
            {
                Filled?.Invoke(this);
                _animator.IdleOpen(false);
                StartCoroutine(_animator.Close());
            }
        }

    }

    public bool IsFull()
    {
        return _currentKeys >= _requiredKeys;
    }
}