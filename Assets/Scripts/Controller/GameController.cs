using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static Structure gameData;
    public GameObject gameOverPanel;
    public bool IsInGame = true;
    private PlayerController player;
    private Enemy[] enemies;
    private CoinCollection coinCollector;
    private Timer timer;
    public static GameController gameController;

    void Awake()
    {
        if (gameController == null)
        {
            gameController = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameData = new Structure(0, 0, 0);

        gameOverPanel.SetActive(false);
        player = FindObjectOfType<PlayerController>();
        enemies = FindObjectsOfType<Enemy>();
        coinCollector = FindObjectOfType<CoinCollection>();
        timer = FindObjectOfType<Timer>();
    }

    void Start()
    {
        gameData = new Structure((int)timer.RemainingTime, 0, 0);

        // Vérifie si la scène actuelle est "gameScene" avant de chercher CoinSpawner
        if (SceneManager.GetActiveScene().name == "gameScene")
        {
            FindObjectOfType<CoinSpawner>().SpawnCoins();
        }
    }

    public void GameOver()
    {
        IsInGame = false;
        gameOverPanel.SetActive(true);

        // Désactiver les mouvements du joueur et des ennemis
        if (player != null)
        {
            player.enabled = false;
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = false;
        }
    }

    public void RestartGame()
    {
        IsInGame = true;
        gameOverPanel.SetActive(false);
        gameData = new Structure(0, 0, 0);

        // Réinitialiser le joueur
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, -22);     // Remet le joueur à la position de départ
            player.UpdateLife(120);                                 // Redonne sa vie au joueur
            player.enabled = true;                                  // Réactive le mouvement
            player.SetMoveSpeed();
        }

        // Réinitialiser les ennemis
        foreach (Enemy enemy in enemies)
        {
            enemy.transform.position = enemy.waypoints[0].position; // Les ennemis reviennent au premier waypoint
            enemy.enabled = true;
            enemy.gameObject.SetActive(true);
        }

        // Réinitialiser les pièces
        coinCollector.ResetCoins();
        FindObjectOfType<CoinSpawner>().SpawnCoins();

        // Réinitialiser le Timer
        timer.ResetTimer();
        Time.timeScale = 1;
    }

    public void Teleporter(int requiredCoins)
    {
        if (coinCollector.GetCoinCount() >= requiredCoins) // Vérifie le nombre de pièces
        {
            gameData.time = (int)timer.RemainingTime;
            gameData.coinCollected = coinCollector.GetCoinCount();
            gameData.enemiesDestroyed = player.enemieDestroyed;
            SceneManager.LoadScene("EndScene");            // Charge la scène de fin
        }
        else
        {
            Debug.Log("Pas assez de pièces pour accéder à EndScene !");
        }
    }
}