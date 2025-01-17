using UnityEngine;

namespace Animations
{
    public static class AnimationData
    {
        public static class Params
        {
            private const string PullingOutParameter = "isPullingOut";
            private const string TurningParameter = "isTurning";
            private const string TryTurningParameter = "isTryTurning";
            private const string UnlockingParameter = "isUnlocking";
            private const string AppearancingParameter = "isAppearancing";
            private const string DisappearancingParameter = "isDisappearancing";
            private const string OpeningParameter = "isOpening";
            private const string ClosingParameter = "isClosing";
            private const string IdleOpeningParameter = "isIdleOpening";
            
            public static readonly int PullOutChisel = Animator.StringToHash(PullingOutParameter);
            public static readonly int TurnKey = Animator.StringToHash(TurningParameter);
            public static readonly int TryTurnKey = Animator.StringToHash(TryTurningParameter);
            public static readonly int UnlockPadlock = Animator.StringToHash(UnlockingParameter);
            public static readonly int AppearanceLockbox = Animator.StringToHash(AppearancingParameter);
            public static readonly int DisappearanceLockbox = Animator.StringToHash(DisappearancingParameter);
            public static readonly int OpenLockbox = Animator.StringToHash(OpeningParameter);
            public static readonly int CloseLockbox = Animator.StringToHash(ClosingParameter);
            public static readonly int IdleOpenLockbox = Animator.StringToHash(IdleOpeningParameter);
        }
    }
}