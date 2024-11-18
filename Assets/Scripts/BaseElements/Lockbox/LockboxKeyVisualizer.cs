using System.Collections.Generic;
using UnityEngine;

public class LockboxKeyVisualizer
{
    private Key _keyVisualPrefab;
    private List<Transform> _keySlots;
    private List<Key> _collectedKeyVisuals = new List<Key>();

    public LockboxKeyVisualizer(List<Transform> keySlots, Key keyVisualPrefab)
    {
        _keySlots = keySlots;
        _keyVisualPrefab = keyVisualPrefab;
    }

    public void UpdateVisual(int currentKeyCount, BaseColor color)
    {
        Create(currentKeyCount, color);
    }

    public void Clear()
    {
        foreach (Key keyVisual in _collectedKeyVisuals)
        {
            if (keyVisual != null)
            {
                keyVisual.gameObject.SetActive(false);
            }
        }

        _collectedKeyVisuals.Clear();
    }

    private void Create(int currentKeyCount, BaseColor color)
    {
        if (currentKeyCount <= _keySlots.Count)
        {
            Transform slot = _keySlots[currentKeyCount - 1];
            Key keyVisual = Key.Instantiate(_keyVisualPrefab, slot.position, slot.rotation, slot);

            keyVisual.Initialize(color);
            keyVisual.transform.localPosition = Vector3.zero;
            keyVisual.transform.localRotation = Quaternion.identity;
            keyVisual.transform.localScale = slot.localScale;
            _collectedKeyVisuals.Add(keyVisual);
        }
    }
}