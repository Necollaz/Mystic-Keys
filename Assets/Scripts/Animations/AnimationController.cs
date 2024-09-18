using UnityEngine;

public static class AnimationData
{
    public static class Params
    {
        public static readonly int OpenDoor = Animator.StringToHash("isOpening");
        public static readonly int PullOutChisel = Animator.StringToHash("isPullingOut");
        public static readonly int TurnKey = Animator.StringToHash("isTurning");
        public static readonly int UnlockPadlock = Animator.StringToHash("isUnlocking");
        public static readonly int AppearanceLockbox = Animator.StringToHash("isAppearancing");
        public static readonly int DisappearanceLockbox = Animator.StringToHash("isDisappearancing");
        public static readonly int OpenLockbox = Animator.StringToHash("isOpening");
        public static readonly int CloseLockbox = Animator.StringToHash("isClosing");
        public static readonly int IdleOpenLockbox = Animator.StringToHash("isIdleOpening");
    }
}

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
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
