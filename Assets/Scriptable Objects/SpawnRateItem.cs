using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnRateItem", menuName = "Unlockable Items/Spawn Rate Item")]
public class SpawnRateItem : UnlockableItem
{
    public float spawnRateMultiplier;
}
