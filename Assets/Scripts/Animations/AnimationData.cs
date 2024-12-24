using UnityEngine;

namespace Animations
{
    public static class AnimationData
    {
        public static class Params 
        {
            public const string PullingOut = "isPullingOut";
            public const string Turning = "isTurning";
            public const string TryTurning = "isTryTurning";
            public const string Unlocking = "isUnlocking";
            public const string Appearancing = "isAppearancing";
            public const string Disappearancing = "isDisappearancing";
            public const string Opening = "isOpening";
            public const string Closing = "isClosing";
            public const string IdleOpening = "isIdleOpening";
    
            public static readonly int PullOutChisel = Animator.StringToHash(PullingOut);
            public static readonly int TurnKey = Animator.StringToHash(Turning);
            public static readonly int TryTurnKey = Animator.StringToHash(TryTurning);
            public static readonly int UnlockPadlock = Animator.StringToHash(Unlocking);
            public static readonly int AppearanceLockbox = Animator.StringToHash(Appearancing);
            public static readonly int DisappearanceLockbox = Animator.StringToHash(Disappearancing);
            public static readonly int OpenLockbox = Animator.StringToHash(Opening);
            public static readonly int CloseLockbox = Animator.StringToHash(Closing);
            public static readonly int IdleOpenLockbox = Animator.StringToHash(IdleOpening);
        }
    }
}
