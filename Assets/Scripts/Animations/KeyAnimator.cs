using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class KeyAnimator : MonoBehaviour
{
    private ControllerAnimations _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
    }

    public IEnumerator TryTurn()
    {
        _animationController.TryTurnKey(true);
        yield return null;

        float animationLength = _animationController.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        _animationController.TryTurnKey(false);
    }

    public IEnumerator Turn()
    {
        _animationController.TurnKey(true);
        yield return null;

        float animationLength = _animationController.GetAnimationLength();
        yield return new WaitForSeconds(animationLength);

        _animationController.TurnKey(false);
    }
}