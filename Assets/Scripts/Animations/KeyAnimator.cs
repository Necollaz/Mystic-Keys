using System;
using System.Collections;
using UnityEngine;

public class KeyAnimator : BaseAnimator
{
    private float _scaleDuration = 0.7f;

    public event Action CollectedComplete;

    public IEnumerator TryTurn()
    {
        ControllerAnimations.TryTurnKey(true);
        yield return null;

        float animationLength = ControllerAnimations.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        ControllerAnimations.TryTurnKey(false);
    }
        
    public IEnumerator Turn()
    {
        ControllerAnimations.TurnKey(true);
        yield return null;

        float animationLength = ControllerAnimations.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        ControllerAnimations.TurnKey(false);
    }

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
}