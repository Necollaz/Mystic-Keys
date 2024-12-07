using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.InventorySystem
{
    [Serializable]
    public class Slot
    {
        [SerializeField] private string _name;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private ParticleSystem _inactiveSlot;
        [SerializeField] private ParticleSystem _reyEffectRemove;
        [SerializeField] private Image _slotImage;
        [SerializeField] private Sprite _defaultSprite;

        public bool IsActive;

        private ParticleSystem _currentParticleSystem;

        public void ActivateSlot()
        {
            IsActive = true;
            _slotImage.sprite = _defaultSprite;

            if (_currentParticleSystem != null)
            {
                _currentParticleSystem.gameObject.SetActive(false);
            }
        }

        public void DeactivateSlot()
        {
            IsActive = false;
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