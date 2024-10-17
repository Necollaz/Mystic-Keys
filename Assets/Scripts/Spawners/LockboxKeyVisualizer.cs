using System.Collections.Generic;
using UnityEngine;

public class LockboxKeyVisualizer
{
    private List<Transform> _keySlots;
    private Key _keyVisualPrefab;
    private List<Key> _collectedKeyVisuals = new List<Key>();

    public LockboxKeyVisualizer(List<Transform> keySlots, Key keyVisualPrefab)
    {
        _keySlots = keySlots;
        _keyVisualPrefab = keyVisualPrefab;
    }

    public void Initialize()
    {
        Clear();
    }

    public void UpdateVisual(int currentKeyCount, BaseColor color)
    {
        if (currentKeyCount <= _keySlots.Count)
        {
            Transform slot = _keySlots[currentKeyCount - 1];
            Key keyVisual = GameObject.Instantiate(_keyVisualPrefab, slot.position, slot.rotation, slot);
            keyVisual.Initialize(color);
            _collectedKeyVisuals.Add(keyVisual);
        }
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
}