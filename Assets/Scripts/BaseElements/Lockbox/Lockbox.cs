using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LockboxAnimator))]
[RequireComponent(typeof(LockboxKeyVisualizer))]
public class Lockbox : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;
    [SerializeField] private List<Transform> _keySlots;
    [SerializeField] private Key _keyVisualPrefab;
    [SerializeField] private int _requiredKeys = 3;

    private int _currentKeys = 0;
    private Renderer _renderer;
    private LockboxAnimator _animator;
    private ApplyColorService _applyColorService;
    private LockboxKeyVisualizer _keyVisualizer;

    public BaseColor Color { get; private set; }
    public int CurrentKeyCount => _currentKeys;

    public event Action<Lockbox, int> KeyAdded;
    public event Action<Lockbox> Filled;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _animator = GetComponent<LockboxAnimator>();
        _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
        _keyVisualizer = new LockboxKeyVisualizer(_keySlots, _keyVisualPrefab);
    }

    public void Initialize(BaseColor color)
    {
        Color = color;
        _currentKeys = 0;

        _applyColorService.Apply(_renderer, Color);
        _animator.Appearance();
        _keyVisualizer.Initialize();

        StartCoroutine(_animator.Open());
    }

    public void AddKey()
    {
        if (_currentKeys < _requiredKeys)
        {
            _currentKeys++;
            KeyAdded?.Invoke(this, _currentKeys);
            _keyVisualizer.UpdateVisual(_currentKeys, Color);

            if (IsFull())
            {
                StartCoroutine(CloseAndNotify());
                _keyVisualizer.Clear();
            }
        }
    }

    public bool IsFull()
    {
        return _currentKeys >= _requiredKeys;
    }

    private IEnumerator CloseAndNotify()
    {
        yield return StartCoroutine(_animator.Close());
        Filled?.Invoke(this);
    }
}