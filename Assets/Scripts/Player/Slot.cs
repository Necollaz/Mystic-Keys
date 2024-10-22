using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Slot
{
    public string Name;
    public bool IsActive;
    public Transform Transform;
    public ParticleSystem InactiveSlot;
    public ParticleSystem KeyAddedEffect;
    public ParticleSystem KeyRemovedEffect;
    public Image SlotImage;
    public Sprite DefaultSprite;
    public Color DefaultColor = Color.grey;
}