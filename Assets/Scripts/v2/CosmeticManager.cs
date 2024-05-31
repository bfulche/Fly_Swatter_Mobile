using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public SwatterItem equippedSwatter;
    public BackgroundItem equippedBackground;
    public List<FlyItem> equippedFlyItems = new List<FlyItem>();

    // References to game objects that will be affected by cosmetic changes
    public SpriteRenderer swatterRenderer;
    public SpriteRenderer backgroundRenderer;
    //public AudioSource swatterAudioSource;

    private void Start()
    {
        // Apply initial cosmetic settings based on equipped items
        ApplySwatterCosmetics();
    }

    private void Update()
    {
        SetBackgroundSprite();
    }

    public void EquipSwatter(SwatterItem swatterItem)
    {
        equippedSwatter = swatterItem;
        ApplySwatterCosmetics();
    }

    public void EquipBackground(BackgroundItem backgroundItem)
    {
        equippedBackground = backgroundItem;
        SetBackgroundSprite();
    }

    public void EquipFly(FlyItem flyItem)
    {
        // Add the fly item to the list of equipped fly items
        if (!equippedFlyItems.Contains(flyItem))
        {
            equippedFlyItems.Add(flyItem);
        }
    }

    public void UnequipFly(FlyItem flyItem)
    {
        // Remove the fly item from the list of equipped fly items
        equippedFlyItems.Remove(flyItem);
    }

    private void ApplySwatterCosmetics()
    {
        if (equippedSwatter != null && swatterRenderer != null)
        {
            swatterRenderer.sprite = equippedSwatter.swatterSprite;
            //swatterAudioSource.clip = equippedSwatter.swatterSound;
        }
    }

    private void SetBackgroundSprite()
    {
        if (backgroundRenderer != null && equippedBackground != null)
        {
            backgroundRenderer.sprite = equippedBackground.backgroundImage;
        }
    }
}