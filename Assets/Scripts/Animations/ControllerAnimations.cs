using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControllerAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    public void OpenDoor(bool isOpening)
    {
        _animator.SetBool(AnimationData.Params.OpenDoor, isOpening);
    }

    public void PullOutChisel(bool isPullingOut)
    {
        _animator.SetBool(AnimationData.Params.PullOutChisel, isPullingOut);
    }

    public void TurnKey(bool isTurning)
    {
        _animator.SetBool(AnimationData.Params.TurnKey, isTurning);
    }

    public void TryTurnKey(bool isTryTurning)
    {
        _animator.SetBool(AnimationData.Params.TryTurnKey, isTryTurning);
    }

    public void RotateKey(bool isRotating)
    {
        _animator.SetBool(AnimationData.Params.RotateKey, isRotating);
    }

    public void UnlockPadlock(bool isUnlocking)
    {
        _animator.SetBool(AnimationData.Params.UnlockPadlock, isUnlocking);
    }

    public void AppearanceLockbox(bool isAppearancing)
    {
        _animator.SetBool(AnimationData.Params.AppearanceLockbox, isAppearancing);
    }

    public void DisappearanceLockbox(bool isDisappearancing)
    {
        _animator.SetBool(AnimationData.Params.DisappearanceLockbox, isDisappearancing);
    }

    public void OpenLockbox(bool isOpening)
    {
        _animator.SetBool(AnimationData.Params.OpenLockbox, isOpening);
    }

    public void CloseLockbox(bool isClosing)
    {
        _animator.SetBool(AnimationData.Params.CloseLockbox, isClosing);
    }

    public void IdleOpenLockbox(bool isIdleOpening)
    {
        _animator.SetBool(AnimationData.Params.IdleOpenLockbox, isIdleOpening);
    }
}