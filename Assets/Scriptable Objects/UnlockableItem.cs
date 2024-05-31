using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnlockableItem", menuName = "Unlockable Items/Base Item")]
public class UnlockableItem : ScriptableObject
{



    public string itemName;
    public string itemDescription;
    public int unlockRequirement;
    public Sprite itemIcon;



}
