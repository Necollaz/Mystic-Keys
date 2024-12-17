using System.Collections.Generic;
using System.Linq;
using BaseElements.FolderKey;
using BaseElements.FolderLockbox;
using Menu;
using Player.InventorySystem;
using Spawners.SpawnerLockboxes;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        private const int MouseButtonDown = 0;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private LockboxRegistry _lockboxRegistry;
        [SerializeField] private PauseMenu _pauseMenu;

        private void Update()
        {
            TryPickupKey();
        }

        private void TryPickupKey()
        {
            if (_pauseMenu.IsPaused)
            {
                return;
            }

            if (Input.GetMouseButtonDown(MouseButtonDown))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if(hit.collider.TryGetComponent(out Key key))
                    {
                        if (key.IsInteractive)
                        {
                            List<Lockbox> activeLockboxes = _lockboxRegistry.GetActive()
                               .Where(lockbox => lockbox.Color == key.Color && lockbox.CurrentKeyCount < lockbox.RequiredKeys)
                               .ToList();

                            if (activeLockboxes.Any())
                            {
                                bool added = activeLockboxes.First().AddKey();

                                if (added)
                                {
                                    UseActiveKey(key);
                                }
                                else if (_inventory.HasSpace())
                                {
                                    UseActiveKey(key);
                                    _inventory.AddKey(key);
                                }
                                else
                                {
                                    UseInactiveKey(key);
                                }
                            }
                            else if (_inventory.HasSpace())
                            {
                                UseActiveKey(key);
                                _inventory.AddKey(key);
                            }
                            else
                            {
                                UseInactiveKey(key);
                            }
                        }
                        else
                        {
                            UseInactiveKey(key);
                        }
                    }
                }
            }
        }

        private void UseActiveKey(Key key)
        {
            key.UseActive();
        }

        private void UseInactiveKey(Key key)
        {
            key.UseInactive();
        }
    }
}