using System;
using UnityEngine;

[RequireComponent(typeof(PadlockAnimator))]
public class Padlock : MonoBehaviour
{
    private PadlockAnimator _padlockAnimator;

    public bool IsUnlocked { get; private set; }
    public int GroupIndex { get; set; }

    public event Action<Padlock> UnlockCompleted;

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