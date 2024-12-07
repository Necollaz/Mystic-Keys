using System.Collections.Generic;
using System.Linq;
using BaseElements.FolderKey;
using BaseElements.FolderLockbox;
using Player.InventorySystem;
using Spawners.SpawnerLockboxes;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private const int GetMouseButtonDown = 0;

        [SerializeField] private Camera _mainCamera;
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
                    if (key.IsInteractive)
                    {
                        List<Lockbox> activeLockboxes = _lockboxRegistry.GetActive()
                           .Where(lb => lb.Color == key.Color && lb.CurrentKeyCount < lb.RequiredKeys)
                           .ToList();

                        if (activeLockboxes.Any())
                        {
                            bool added = activeLockboxes.First().AddKey();

                            if (added)
                            {
                                key.UseActive();
                            }
                            else if (_inventory.HasSpace())
                            {
                                key.UseActive();
                                _inventory.AddKey(key);
                            }
                            else
                            {
                                key.UseInactive();
                            }
                        }
                        else if (_inventory.HasSpace())
                        {
                            key.UseActive();
                            _inventory.AddKey(key);
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
}