using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LockboxAnimator))]
public class Lockbox : MonoBehaviour
{
    [SerializeField] private ColorMaterialPair[] _colorMaterials;
    [SerializeField] private List<Transform> _keySlots;
    [SerializeField] private Key _keyVisualPrefab;
    [SerializeField] private int _requiredKeys = 3;

    private Renderer _renderer;
    private LockboxAnimator _animator;
    private ApplyColorService _applyColorService;
    private LockboxKeyVisualizer _keyVisualizer;
    private int _currentKeys = 0;

    public event Action<Lockbox, int> KeyAdded;
    public event Action<Lockbox> Filled;
    public event Action<Lockbox> Opened;

    public BaseColor Color { get; private set; }
    public int CurrentKeyCount => _currentKeys;
    public int RequiredKeys => _requiredKeys;

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
        _keyVisualizer.Clear();

        StartCoroutine(Initialize());
    }

    public void AddKey()
    {
        if (_currentKeys < _requiredKeys)
        {
            _currentKeys++;
            _keyVisualizer.UpdateVisual(_currentKeys, Color);

            if (IsFull())
            {
                StartCoroutine(Fill());
            }

            KeyAdded?.Invoke(this, _currentKeys);
        }
    }

    public bool IsFull()
    {
        return _currentKeys >= _requiredKeys;
    }

    public Transform GetAvailableSlot()
    {
        return _keySlots[_currentKeys];
    }

    private IEnumerator Fill()
    {
        _keyVisualizer.Clear();

        yield return StartCoroutine(_animator.Close());
        Filled?.Invoke(this);
    }

    private IEnumerator Initialize()
    {
        yield return StartCoroutine(_animator.Appearance());
        yield return StartCoroutine(_animator.Open());
        Opened?.Invoke(this);
    }
}