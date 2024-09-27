using UnityEngine;

public class InventorySlot
{
    private Transform _position;
    private Key _key;

    public void Add(Key key)
    {
        _key = key;
    }

    public void Remove()
    {
        _key = null;
    }

    public Key Get()
    {
        return _key;
    }

    public bool IsEmpty()
    {
        return _key == null;
    }
}
