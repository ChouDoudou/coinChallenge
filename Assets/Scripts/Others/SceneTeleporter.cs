using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] public int requiredCoins = 10; // Nombre minimum de pièces pour la téléportation
    private CoinCollection coinCollector;
    

    void Start()
    {
        coinCollector = FindObjectOfType<CoinCollection>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameController.Teleporter(requiredCoins);
        }
    }
}