using System;
using System.Collections;
using UnityEngine;
using Animations;
using ColorService;

namespace BaseElements.FolderKey
{
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
        
        public event Action<Key> Collected;
        
        public BaseColors Color { get; private set; }
        public int LayerIndex { get; set; }
        public int GroupIndex { get; set; }
        public bool IsInteractive { get; private set; }
        public bool IsPickedUp => _isPickedUp;
        
        private void Awake()
        {
            _animator = GetComponent<KeyAnimator>();
            _collider = GetComponent<Collider>();
            _renderer = GetComponentInChildren<Renderer>();
            
            _applyColorService = new ApplyColorService { ColorMaterials = _colorMaterials };
            
            _animator.CollectedComplete += OnAnimationComplete;
        }
        
        private void OnDestroy()
        {
            _animator.CollectedComplete -= OnAnimationComplete;
        }
        
        public void Initialize(BaseColors color)
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
            if (_isPickedUp == false)
            {
                _isPickedUp = true;
                
                if (_collider != null)
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
            if (!_isPickedUp)
            {
                return;
            }
            
            Collected?.Invoke(this);
        }
    }
}