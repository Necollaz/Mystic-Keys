using System;
using UnityEngine;

[Serializable]
public class Slot
{
    public Transform Transform;
    public bool IsActive;
    public ParticleSystem InactiveSlot;
}