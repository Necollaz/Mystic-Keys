using System;
using System.Collections.Generic;
using UnityEngine;

public class LockboxRegistry : MonoBehaviour
{
    private readonly List<Lockbox> _activeLockboxes = new List<Lockbox>();
    
    public event Action<Lockbox> LockboxCreated;
    public event Action<Lockbox> LockboxFilled;

    public void Register(Lockbox lockbox)
    {
        if (_activeLockboxes.Contains(lockbox) == false)
        {
            _activeLockboxes.Add(lockbox);
            lockbox.Filled += OnFilled;
            LockboxCreated?.Invoke(lockbox);
        }
    }

    public void Unregister(Lockbox lockbox)
    {
        if (_activeLockboxes.Contains(lockbox))
        {
            _activeLockboxes.Remove(lockbox);
            lockbox.Filled -= OnFilled;
        }
    }

    public void Clear()
    {
        foreach (Lockbox lockbox in _activeLockboxes)
        {
            lockbox.Filled -= OnFilled;
        }

        _activeLockboxes.Clear();
    }

    public List<Lockbox> GetActive()
    {
        return _activeLockboxes;
    }

    private void OnFilled(Lockbox filledLockbox)
    {
        LockboxFilled?.Invoke(filledLockbox);
    }
}