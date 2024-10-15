using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Slot
{
    public Transform Transform;
    public ParticleSystem InactiveSlot;
    public Image SlotImage;
    public Sprite DefaultSprite;
    public Color DefaultColor = Color.grey;
    public bool IsActive;
}