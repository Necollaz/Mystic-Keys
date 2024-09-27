using System;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class Padlock : MonoBehaviour
{
    private ControllerAnimations _animator;

    public event Action<Padlock> OnUnlocked;

    public bool IsUnlocked { get; private set; }

    public void Unlock()
    {
        if (!IsUnlocked)
        {
            IsUnlocked = true;
            _animator.UnlockPadlock(true);
            OnUnlocked?.Invoke(this);
        }
    }
}