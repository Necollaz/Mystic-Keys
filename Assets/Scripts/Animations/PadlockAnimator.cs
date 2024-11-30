using System;

public class PadlockAnimator : BaseAnimator
{
    private float _scaleDuration = 0.5f;

    public event Action UnlockComplete;

    public override void TriggerAnimation()
    {
        ControllerAnimations.UnlockPadlock(true);
    }

    public override float GetScaleDuration()
    {
        return _scaleDuration;
    }

    public override void OnAnimationComplete()
    {
        UnlockComplete?.Invoke();
    }
}