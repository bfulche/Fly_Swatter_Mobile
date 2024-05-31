using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockableTile : MonoBehaviour
{
    public UnlockableItem unlockableItem;
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text requirementText;
    public Button unlockButton;

    private void Start()
    {
        // Populate the UI elements based on the assigned UnlockableItem
        if (unlockableItem != null)
        {
            iconImage.sprite = unlockableItem.itemIcon;
            nameText.text = unlockableItem.itemName;
            //descriptionText.text = unlockableItem.itemDescription;
            requirementText.text = "Unlock Requirement: " + unlockableItem.unlockRequirement.ToString();
        }
    }

    public void OnUnlockButtonClicked()
    {
        // Handle the unlock button click event
        // You can add your unlock logic here, such as checking if the player meets the unlock requirement
        // and updating the UI accordingly
        Debug.Log("Unlock button clicked for item: " + unlockableItem.itemName);
    }
}
