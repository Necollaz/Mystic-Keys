using UnityEngine;

public class RotationLogic
{
    protected float _speed;

    public RotationLogic(float speed)
    {
        _speed = speed;
    }

    public void Rotate(Transform target, float mouseX)
    {
        Vector3 localRotation = new Vector3(0, mouseX, 0) * _speed;
        target.Rotate(localRotation, Space.World);
    }
}