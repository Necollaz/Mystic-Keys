using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Slot
{
    public string Name;
    public bool IsActive;
    public RectTransform Transform;
    public ParticleSystem InactiveSlot;
    public ParticleSystem KeyEffect;
    public Image SlotImage;
    public Sprite DefaultSprite;

    public void ActivateSlot()
    {
        IsActive = true;
        SlotImage.sprite = DefaultSprite;
    }

    public void DeactivateSlot()
    {
        IsActive = false;
        SlotImage.sprite = DefaultSprite;
        CreateParticalSystem(InactiveSlot);
    }

    public void SetKeySprite(Sprite keySprite)
    {
        SlotImage.sprite = keySprite;
        CreateParticalSystem(KeyEffect);
    }

    public void ResetSprite()
    {
        SlotImage.sprite = DefaultSprite;
        CreateParticalSystem(KeyEffect);
    }

    private void CreateParticalSystem(ParticleSystem particleSystem)
    {
        ParticleSystem.Instantiate(particleSystem, Transform.position, Quaternion.identity, Transform);
    }
}