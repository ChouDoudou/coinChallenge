using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI enemiesText;

    // Start is called before the first frame update
    void Start()
    {
        Structure data = GameController.gameData;
        timeText.text = "Temps restant : " + Timer.GetDigitTime(data.time);
        coinText.text = "Pièces récoltées : " + data.coinCollected.ToString();
        enemiesText.text = "Ennemis éliminés : " + data.enemiesDestroyed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
