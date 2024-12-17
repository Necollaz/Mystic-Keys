using Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorService;
using BaseElements.FolderKey;

namespace BaseElements.FolderLockbox
{
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

        public BaseColors Color { get; private set; }
        public int CurrentKeyCount => _currentKeys;
        public int RequiredKeys => _requiredKeys;

        private void Awake()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _animator = GetComponent<LockboxAnimator>();

            _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
            _keyVisualizer = new LockboxKeyVisualizer(_keySlots, _keyVisualPrefab);
        }

        public void Initialize(BaseColors color)
        {
            Color = color;
            _currentKeys = 0;

            _applyColorService.Apply(_renderer, Color);
            _keyVisualizer.Clear();

            StartCoroutine(InitializeCoroutine());
        }

        public bool AddKey()
        {
            if (_currentKeys < _requiredKeys)
            {
                _currentKeys++;
                _keyVisualizer.UpdateVisual(_currentKeys, Color);

                if (_currentKeys >= _requiredKeys)
                {
                    StartCoroutine(Fill());
                }

                KeyAdded?.Invoke(this, _currentKeys);

                return true;
            }

            return false;
        }

        private IEnumerator Fill()
        {
            _keyVisualizer.Clear();

            yield return StartCoroutine(_animator.Close());

            Filled?.Invoke(this);
        }

        private IEnumerator InitializeCoroutine()
        {
            yield return StartCoroutine(_animator.Appearance());
            yield return StartCoroutine(_animator.Open());

            Opened?.Invoke(this);
        }
    }
}