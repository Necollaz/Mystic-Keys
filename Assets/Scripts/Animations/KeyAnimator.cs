using UnityEngine;

[RequireComponent(typeof(ControllerAnimations))]
public class KeyAnimator : MonoBehaviour
{
    private ControllerAnimations _animationController;

    private void Awake()
    {
        _animationController = GetComponent<ControllerAnimations>();
    }

    public void Rotate(bool isRotate)
    {
        _animationController.RotateKey(isRotate);
    }

    public void Turn(bool isActive)
    {
        _animationController.TurnKey(isActive);
    }

    public void TryTurn(bool isActive)
    {
        _animationController.TryTurnKey(isActive);
    }
}