using UnityEngine;

namespace Animations
{
    public static class AnimationData
    {
        public static class Params 
        {
            private const string PullingOut = "isPullingOut";
            private const string Turning = "isTurning";
            private const string TryTurning = "isTryTurning";
            private const string Unlocking = "isUnlocking";
            private const string Appearancing = "isAppearancing";
            private const string Disappearancing = "isDisappearancing";
            private const string Opening = "isOpening";
            private const string Closing = "isClosing";
            private const string IdleOpening = "isIdleOpening";
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