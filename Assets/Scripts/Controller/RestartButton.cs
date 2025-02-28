using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private GameController gameController;
    private Button button;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnRestartButtonClick);
    }

    public void OnRestartButtonClick()
    {
        Debug.Log("Restart");
        gameController.RestartGame();
    }
}