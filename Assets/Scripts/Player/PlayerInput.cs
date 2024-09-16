using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private float _rotationSpeed = 5f;

    private Rotator _objectRotator;

    private void Start()
    {
        _objectRotator = new Rotator(_rotationSpeed);
    }

    private void Update()
    {
        _objectRotator.TryRotating(_door.transform);
    }
}
