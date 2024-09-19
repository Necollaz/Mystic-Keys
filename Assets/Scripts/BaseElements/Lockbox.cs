using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class Lockbox : MonoBehaviour
{
    private PoolKeys _keyPool;
    private ControllerAnimations _animationController;
    private BaseColor _color;
    private int _requiredKeys = 3;
    private int _currentKeys = 0;
    private float _waitTime = 1f;

    public event Action<Lockbox> OnLockboxFilled;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
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
                StartCoroutine(Close());
            }
        }
    }

    private IEnumerator Full()
    {
        yield return Wait();

        OnLockboxFilled?.Invoke(this);
    }

    private IEnumerator Open()
    {
        yield return Wait();

        _animationController.AppearanceLockbox(false);
        _animationController.OpenLockbox(true);

        yield return Wait();

        _animationController.OpenLockbox(false);
        _animationController.IdleOpenLockbox(true);
    }

    private IEnumerator Close()
    {
        _animationController.CloseLockbox(true);

        yield return Wait();

        _animationController.CloseLockbox(false);
        _animationController.DisappearanceLockbox(true);

        StartCoroutine(Full());
    }

    private WaitForSeconds Wait()
    {
        return new WaitForSeconds(_waitTime);
    }
}