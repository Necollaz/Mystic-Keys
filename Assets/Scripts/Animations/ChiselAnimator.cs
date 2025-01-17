using System;

namespace Animations
{
    public class ChiselAnimator : BaseAnimator
    {
        private float _scaleDuration = 0.7f;
        
        public event Action PullOutComplete;
        
        public override void TriggerAnimation()
        {
            ControllerAnimations.SetBool(AnimationData.Params.PullOutChisel, true);
        }
        
        public override float GetScaleDuration()
        {
            return _scaleDuration;
        }
        
        public override void OnAnimationComplete()
        {
            PullOutComplete?.Invoke();
        }
    }
}