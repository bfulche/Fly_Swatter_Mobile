using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShop : MonoBehaviour
{
    public Transform contentContainer;
    public GameObject unlockableTilePrefab;
    public List<UnlockableItem> unlockableItems;

    private void Start()
    {
        // Populate the Unlock Shop with UI tiles
        PopulateShop();
    }

    private void PopulateShop()
    {
        // Clear any existing UI tiles
        foreach (Transform child in contentContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new UI tile for each UnlockableItem
        foreach (UnlockableItem item in unlockableItems)
        {
            GameObject tileInstance = Instantiate(unlockableTilePrefab, contentContainer);
            UnlockableTile unlockableTile = tileInstance.GetComponent<UnlockableTile>();
            if (unlockableTile != null)
            {
                unlockableTile.unlockableItem = item;
            }
        }
    }
}
