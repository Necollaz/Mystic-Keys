using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlotData", menuName = "ScriptableObjects/SlotData", order = 1)]
public class SlotData : ScriptableObject
{
    public List<int> PurchasedLockboxSlots = new List<int>();
    public List<int> PurchasedInventorySlots = new List<int>();
}