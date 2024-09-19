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