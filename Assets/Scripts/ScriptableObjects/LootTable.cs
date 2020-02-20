using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;


    public PowerUp LootPowerUp()
    {
        int cumilativeProbability = 0;
        int currentProbability = Random.Range(0, 100);
        for(int i = 0; i < loots.Length; i++) {
            cumilativeProbability += loots[i].lootChance;
            if( currentProbability <= cumilativeProbability) {
                return loots[i].thisLoot;
            }
        }

        return null;
    }
    
}
