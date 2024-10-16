using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class LockboxAnimator : MonoBehaviour
{
    private ControllerAnimations _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
    }

    public IEnumerator Open()
    {
        yield return Wait();

        _animationController.AppearanceLockbox(false);
        _animationController.OpenLockbox(true);

        yield return Wait();

        _animationController.OpenLockbox(false);
        _animationController.IdleOpenLockbox(true);
    }

    public IEnumerator Close()
    {
        _animationController.IdleOpenLockbox(false);
        _animationController.CloseLockbox(true);

        yield return Wait();

        _animationController.CloseLockbox(false);
        _animationController.DisappearanceLockbox(true);

        yield return Wait();
    }

    public void Appearance()
    {
        _animationController.AppearanceLockbox(true);
    }

    public void IdleOpen(bool isIdle)
    {
        _animationController.IdleOpenLockbox(isIdle);
    }

    private WaitForSeconds Wait()
    {
        float animationLength = _animationController.GetAnimationLength();

        return new WaitForSeconds(animationLength);
    }
}