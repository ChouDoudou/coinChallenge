using System.Collections;
using System.Collections.Generic;
using System.Xml;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]  
public class CoinSpawnData // Ne pas hériter de MonoBehaviour
{
    public GameObject prefab; // La pièce (normale, boost ou grande pièce)
    public Transform spawnPointA; // Premier point de spawn
    public Transform spawnPointB; // Deuxième point de spawn
}

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinCollection coinCollection;
    [SerializeField] private List<CoinSpawnData> spawnDataList = new List<CoinSpawnData>(); // Liste des données de spawn

    public void SpawnCoins()
    {
        List<GameObject> coins = new List<GameObject>();
        GameObject coin;
        foreach (CoinSpawnData coinData in spawnDataList)
        {
            // Choix aléatoire entre les deux positions possibles
            Transform chosenPoint = Random.Range(0, 2) == 0 ? coinData.spawnPointA : coinData.spawnPointB;

            // Instanciation de la pièce
            coin = Instantiate(coinData.prefab, chosenPoint);
            coins.Add(coin);

            coin.transform.localPosition = Vector3.zero;
        }
        Debug.Log("Spawn des Coins");
        coinCollection.FakeStart(coins);
    }
}