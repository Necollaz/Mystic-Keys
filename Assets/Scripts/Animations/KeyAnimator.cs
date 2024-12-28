using System;
using System.Collections;
using UnityEngine;

namespace Animations
{
    public class KeyAnimator : BaseAnimator
    {
        private readonly float _scaleDuration = 0.7f;

        public event Action CollectedComplete;

        public override void TriggerAnimation()
        {
            StartCoroutine(Turn());
        }

        public override float GetScaleDuration()
        {
            return _scaleDuration;
        }

        public override void OnAnimationComplete()
        {
            CollectedComplete?.Invoke();
        }

        public IEnumerator TryTurn()
        {
            ControllerAnimations.SetBool(AnimationData.Params.TryTurnKey, true);
            yield return null;

            float animationLength = ControllerAnimations.GetAnimationLength();
            yield return new WaitForSeconds(animationLength);

            ControllerAnimations.SetBool(AnimationData.Params.TryTurnKey, false);
        }
        
        private IEnumerator Turn()
        {
            ControllerAnimations.SetBool(AnimationData.Params.TurnKey, true);
            yield return null;

            float animationLength = ControllerAnimations.GetAnimationLength();
            yield return new WaitForSeconds(animationLength);

            ControllerAnimations.SetBool(AnimationData.Params.TurnKey, false);
        }
    }
}