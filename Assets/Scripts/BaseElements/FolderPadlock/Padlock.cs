using System;
using UnityEngine;
using Animations;

namespace BaseElements.FolderPadlock
{
    [RequireComponent(typeof(PadlockAnimator))]
    public class Padlock : MonoBehaviour
    {
        private PadlockAnimator _padlockAnimator;
        
        public event Action<Padlock> UnlockCompleted;
        
        public int GroupIndex { get; set; }
        public bool IsUnlocked { get; private set; }
        
        private void Awake()
        {
            _padlockAnimator = GetComponent<PadlockAnimator>();
            
            _padlockAnimator.UnlockComplete += OnUnlockComplete;
        }
        
        public void Reset()
        {
            IsUnlocked = false;
        }
        
        public void Unlock()
        {
            if (IsUnlocked == false)
            {
                IsUnlocked = true;
                
                _padlockAnimator.PlayAnimation();
            }
        }
        
        private void OnUnlockComplete()
        {
            _padlockAnimator.UnlockComplete -= OnUnlockComplete;
            
            gameObject.SetActive(false);
            UnlockCompleted?.Invoke(this);
        }
    }
}