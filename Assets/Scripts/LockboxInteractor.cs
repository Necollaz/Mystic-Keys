using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LockboxInteractor : MonoBehaviour
{
    private KeyInventory _keyInventory;
    private Effects _effects;

    public void Initialize(KeyInventory keyInventory, Effects effects)
    {
        _keyInventory = keyInventory;
        _effects = effects;
    }

    public void Move(Lockbox lockbox)
    {
        int keysNeeded = lockbox.RequiredKeys - lockbox.CurrentKeyCount;

        List<KeyValuePair<Slot, Key>> keysToMove = _keyInventory.GetByColor(lockbox.Color)
            .Take(keysNeeded)
            .ToList();

        foreach (KeyValuePair<Slot, Key> pair in keysToMove)
        {
            Slot slot = pair.Key;
            Key key = pair.Value;
            StartCoroutine(MoveKeyToLockbox(key, slot, lockbox));
        }
    }

    private IEnumerator MoveKeyToLockbox(Key key, Slot slot, Lockbox lockbox)
    {
        Vector3 startPosition = slot.Transform.position;
        Transform targetSlot = lockbox.GetAvailableSlot();
        Vector3 endPosition = targetSlot.position;

        Key keyInstance = Instantiate(key, startPosition, Quaternion.identity);
        keyInstance.gameObject.SetActive(true);

        float duration = 1.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            keyInstance.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        keyInstance.transform.position = endPosition;
        keyInstance.gameObject.SetActive(false);
        _keyInventory.Remove(slot);
        lockbox.AddKey();
        _effects.Play(slot);
    }
}