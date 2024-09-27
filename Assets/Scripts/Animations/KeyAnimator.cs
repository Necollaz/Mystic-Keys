using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class KeyAnimator : MonoBehaviour
{
    private ControllerAnimations _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
    }

    public void Rotate()
    {
        _animationController.RotateKey(true);
    }

    public void Turn()
    {
        _animationController.TurnKey(true);
    }

    public void TryTurn()
    {
        _animationController.TryTurnKey(true);
    }

    public void StopRotate()
    {
        _animationController.RotateKey(false);
    }
}