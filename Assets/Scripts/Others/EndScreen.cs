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
        timeText.text = Timer.GetDigitTime(data.time);
        coinText.text = data.coinCollected.ToString();
        enemiesText.text = data.enemiesDestroyed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
