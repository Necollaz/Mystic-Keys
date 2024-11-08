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

        _animationController.DisappearanceLockbox(false);

        yield return Wait();
    }

    public IEnumerator Appearance()
    {
        _animationController.AppearanceLockbox(true);
        yield return Wait();
        _animationController.AppearanceLockbox(false);
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