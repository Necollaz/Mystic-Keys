using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const int GetMouseButtonDown = 0;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private KeyLayer _keyLayer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LockboxRegistry _lockboxRegistry;

    private void Update()
    {
        if (Input.GetMouseButtonDown(GetMouseButtonDown))
        {
            TryPickupKey();
        }    
    }

    private void TryPickupKey()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out Key key))
            {
                if (_keyLayer.IsCurrent(key.LayerIndex))
                {
                    List<Lockbox> activeLockboxes = _lockboxRegistry.GetActive();
                    Lockbox matchingLockbox = activeLockboxes.FirstOrDefault(lockbox => lockbox.Color == key.Color);

                    if (matchingLockbox != null)
                    {
                        key.UseActive();
                        matchingLockbox.AddKey();
                        _keyLayer.Unregister(key.LayerIndex, key);
                    }
                    else if(_inventory.HasSpace())
                    {
                        key.UseActive();
                        _inventory.AddKey(key);
                        _keyLayer.Unregister(key.LayerIndex, key);
                    }
                    else
                    {
                        key.UseInactive();
                    }
                }
                else
                {
                    key.UseInactive();
                }
            }
        }
    }
}