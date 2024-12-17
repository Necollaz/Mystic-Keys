using System;
using System.Collections.Generic;
using BaseElements.FolderLockbox;
using UnityEngine;

namespace Spawners.SpawnerLockboxes
{
    public class LockboxRegistry : MonoBehaviour
    {
        private readonly List<Lockbox> _activeLockboxes = new List<Lockbox>();
    
        public event Action<Lockbox> LockboxCreated;
        public event Action<Lockbox> LockboxFilled;

        public List<Lockbox> GetActive()
        {
            return _activeLockboxes;
        }

        public void Add(Lockbox lockbox)
        {
            if (_activeLockboxes.Contains(lockbox) == false)
            {
                _activeLockboxes.Add(lockbox);

                lockbox.Filled += OnFilled;

                LockboxCreated?.Invoke(lockbox);
            }
        }

        public void Remove(Lockbox lockbox)
        {
            if (_activeLockboxes.Contains(lockbox))
            {
                _activeLockboxes.Remove(lockbox);

                lockbox.Filled -= OnFilled;
            }
        }

        private void OnFilled(Lockbox filledLockbox)
        {
            LockboxFilled?.Invoke(filledLockbox);
        }
    }
}