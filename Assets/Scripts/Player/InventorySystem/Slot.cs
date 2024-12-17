using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.InventorySystem
{
    [Serializable]
    public class Slot
    {
        [SerializeField] private string _name;
        [SerializeField] private bool _isActive;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _slotImage;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private ParticleSystem _inactiveSlot;
        [SerializeField] private ParticleSystem _reyEffectRemove;

        private ParticleSystem _currentParticleSystem;

        public bool IsActive => _isActive;

        public void Activate()
        {
            _isActive = true;
            _slotImage.sprite = _defaultSprite;

            if (_currentParticleSystem != null)
            {
                _currentParticleSystem.gameObject.SetActive(false);
                _currentParticleSystem = null;
            }
        }

        public void Deactivate()
        {
            _isActive = false;
            _slotImage.sprite = _defaultSprite;
            CreateParticalSystem(_inactiveSlot);
        }

        public void SetKeySprite(Sprite keySprite)
        {
            _slotImage.sprite = keySprite;
            CreateParticalSystem(_reyEffectRemove);
        }

        public void ResetSprite()
        {
            _slotImage.sprite = _defaultSprite;
            CreateParticalSystem(_reyEffectRemove);
        }

        private void CreateParticalSystem(ParticleSystem particleSystemPrefab)
        {
            if (_currentParticleSystem != null)
            {
                return;
            }

            _currentParticleSystem = ParticleSystem.Instantiate(particleSystemPrefab, _rectTransform.position, Quaternion.identity, _rectTransform);
        }
    }
}