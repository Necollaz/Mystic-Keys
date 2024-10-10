using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private KeyLayer _keyLayer;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private LockboxRegistry _lockboxRegistry;
    [SerializeField] private ParticleSystem _removeKey;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickupKey();
        }    
    }

    private void TryPickupKey()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out Key key) && key.TryGetComponent(out KeyAnimator animator))
            {
                if (_keyLayer.IsCurrentLayer(key.LayerIndex))
                {
                    animator.Turn();

                    List<Lockbox> activeLockboxes = _lockboxRegistry.GetActive();
                    Lockbox matchingLockbox = activeLockboxes.FirstOrDefault(lockbox => lockbox.Color == key.Color);

                    if (matchingLockbox != null)
                    {
                        StartCoroutine(CollectKey(key));
                        _keyLayer.Unregister(key.LayerIndex, key);
                        matchingLockbox.AddKey();
                    }
                    else
                    {
                        StartCoroutine(CollectKey(key));
                        _inventory.AddKey(key);
                    }
                }
                else
                {
                    animator.TryTurn();
                }
            }
        }
    }

    private IEnumerator CollectKey(Key key)
    {
        float animationDuration = 1f;

        yield return new WaitForSeconds(animationDuration);

        Instantiate(_removeKey, key.transform.position, Quaternion.identity);
        key.gameObject.SetActive(false);
    }
}