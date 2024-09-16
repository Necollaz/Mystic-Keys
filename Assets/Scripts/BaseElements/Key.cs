using UnityEngine;

public class Key : MonoBehaviour
{
    private KeyColor _color;

    private bool _isCollected;

    private void Awake()
    {
        _isCollected = false;
    }

    public void Collected()
    {
        if (!_isCollected)
        {
            _isCollected = true;
        }
    }
}
