using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CoinCollection : MonoBehaviour
{
    private PlayerController playerController;
    const string COIN = "Coin";
    const string BOOST = "Boost";
    const string BIGCOIN = "BigCoin";
    private int coin = 0;
    public TextMeshProUGUI coinText;
    [SerializeField] private AudioClip audioClickSound = null;
    private AudioSource audioCoinSound;
    private List<GameObject> coins;

    void Awake()
    {
        audioCoinSound = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
    }

    public void FakeStart(List<GameObject> newcoins)
    {
        coins = newcoins;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(COIN))
        {
            coin++;
            coinText.text = "Coin : " + coin.ToString();
            Debug.Log("Coin : " + coin);
            audioCoinSound.PlayOneShot(audioClickSound);
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag(BOOST))
        {
            playerController.SetSuperSpeed();
            Debug.Log("Boost collected");
            audioCoinSound.PlayOneShot(audioClickSound);
            other.gameObject.SetActive(false);
        }

        if(other.CompareTag(BIGCOIN))
        {
            coin += 2;
            coinText.text = "Coin : " + coin.ToString();
            Debug.Log("Coin : " + coin);
            audioCoinSound.PlayOneShot(audioClickSound);
            other.gameObject.SetActive(false);
        }
    }

    public int GetCoinCount()
    {
        return coin;
    }

    public void ResetCoins()
    {
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        coins.Clear();

        coin = 0;
        coinText.text = "Coin : 0";

        // Rechercher les nouvelles pièces après les avoir respawnées
        StartCoroutine(RepopulateCoinList());
    }

    // Coroutine pour attendre le spawn des nouvelles pièces
    private IEnumerator RepopulateCoinList()
    {
        yield return new WaitForSeconds(0.1f); // Attendre un instant pour s'assurer que le spawn est fait

        coins = GameObject.FindGameObjectsWithTag("Coin").ToList();
        coins.AddRange(GameObject.FindGameObjectsWithTag("Boost").ToList());
        coins.AddRange(GameObject.FindGameObjectsWithTag("BigCoin").ToList());
    }
}