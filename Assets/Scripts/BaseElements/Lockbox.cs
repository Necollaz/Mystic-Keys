using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class Lockbox : MonoBehaviour
{
    [SerializeField] private PoolKeys _keyPool;

    private AnimationController _animationController;
    private BaseColor _color;
    private int _requiredKeys = 3;
    private int _currentKeys = 0;
    private float _waitTime = 1f;

    public event Action<Lockbox> OnLockboxFilled;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
    }

    public void Initialize(BaseColor color)
    {
        _color = color;
        _currentKeys = 0;
        _animationController.AppearanceLockbox(true);
        StartCoroutine(Open());
    }

    public void AddKey(Key key)
    {
        if(key.GetColor() == _color)
        {
            _currentKeys++;
            _keyPool.Return(key);

            if (_currentKeys >= _requiredKeys)
            {
                _animationController.IdleOpenLockbox(false);
                _animationController.CloseLockbox(true);
                _animationController.DisappearanceLockbox(true);
                StartCoroutine(NotifyLockboxFilled());
            }
        }
    }

    private IEnumerator NotifyLockboxFilled()
    {
        yield return new WaitForSeconds(_waitTime);

        OnLockboxFilled?.Invoke(this);
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(_waitTime);

        _animationController.AppearanceLockbox(false);
        _animationController.OpenLockbox(true);
        _animationController.IdleOpenLockbox(true);
    }
}