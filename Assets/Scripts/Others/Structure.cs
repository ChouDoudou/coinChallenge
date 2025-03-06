using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Structure
{
    public int time;
    public int enemiesDestroyed;
    public int coinCollected;

    public Structure(int newTime, int newEnemiesDestroyed, int newCoinCollected)
    {
        time = newTime;
        enemiesDestroyed = newEnemiesDestroyed;
        coinCollected = newCoinCollected;
    }
}