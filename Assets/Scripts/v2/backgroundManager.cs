using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManager : MonoBehaviour
{
    public CosmeticManager cosmeticManager;
    public BackgroundItem newBackgroundItem;

    private void ChangeBackground()
    {
        cosmeticManager.EquipBackground(newBackgroundItem);
    }
}
