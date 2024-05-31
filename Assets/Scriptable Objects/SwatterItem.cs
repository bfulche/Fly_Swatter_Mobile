using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSwatterItem", menuName = "Unlockable Items/Swatter Item")]
public class SwatterItem : UnlockableItem
{
    public Sprite swatterSprite;
    public AudioClip swatterSound;
}
