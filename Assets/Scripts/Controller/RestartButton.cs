using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnRestartButtonClick);
    }

    public void OnRestartButtonClick()
    {
        Debug.Log("Restart");

        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            gameController.RestartGame();
        }
        else
        {
            Debug.LogWarning("GameController absent");
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameScene");
        }
    }
}