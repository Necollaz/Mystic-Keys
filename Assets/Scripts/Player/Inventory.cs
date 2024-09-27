using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private RectTransform[] _slotUI;
    [SerializeField] private int _maxSlots = 4;

    private InventorySlot[] _slots;

    private void Awake()
    {
        _slots = new InventorySlot[_maxSlots];

        for (int i = 0; i < _maxSlots; i++)
        {
            _slots[i] = new InventorySlot();
        }
    }

    public bool AddKey(Key key)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i].IsEmpty())
            {
                _slots[i].Add(key);

                if(key.TryGetComponent(out KeyAnimator animations))
                {
                    animations.Rotate();
                }

                if (i < _slotUI.Length)
                {
                    key.transform.SetParent(_slotUI[i].transform, false);
                }

                return true;
            }
        }

        return false;
    }

    public void RemoveKey(BaseColor color, Lockbox lockbox)
    {
        foreach (InventorySlot slot in _slots)
        {
            if(!slot.IsEmpty() && slot.Get().GetColor() == color)
            {
                Key key = slot.Get();
                slot.Remove();
                lockbox.AddKey(key);
                break;
            }
        }
    }
}