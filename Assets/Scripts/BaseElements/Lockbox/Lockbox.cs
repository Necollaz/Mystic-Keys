using System;
using UnityEngine;

[RequireComponent(typeof(LockboxAnimator))]
public class Lockbox : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;

    private PoolKeys _keyPool;
    private LockboxAnimator _animator;
    private ApplyColorService _applyColorService;
    private Renderer _renderer;
    private BaseColor _color;
    private int _requiredKeys = 3;
    private int _currentKeys = 0;

    public event Action<Lockbox> OnLockboxFilled;

    private void Awake()
    {
        _animator = GetComponent<LockboxAnimator>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void Initialize(BaseColor color)
    {
        _color = color;
        _currentKeys = 0;
        _applyColorService.Apply(_renderer, _color);
        _animator.Appearance();
        StartCoroutine(_animator.Open());
    }

    public BaseColor GetColor()
    {
        return _color;
    }

    public void AddKey(Key key)
    {
        if(key.GetColor() == _color)
        {
            _currentKeys++;
            _keyPool.Return(key);

            if (_currentKeys >= _requiredKeys)
            {
                OnLockboxFilled?.Invoke(this);
                _animator.IdleOpen(false);
                StartCoroutine(_animator.Close());
            }
        }
    }
}