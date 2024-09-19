using UnityEngine;

public class Rotator : RotationLogic
{
    private const string Horizontal = "Mouse X";
    private const int MouseButtonLeft = 0;

    public Rotator(float speed) : base(speed) { }

    public void TryRotating(Transform target)
    {
        if (Input.GetMouseButton(MouseButtonLeft))
        {
            float mouseX = Input.GetAxis(Horizontal);

            Rotate(target, mouseX);
        }
    }
}
