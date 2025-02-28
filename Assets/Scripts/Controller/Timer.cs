using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    public float RemainingTime
    {
        get { return remainingTime; }
    }
    [SerializeField] GameController gameScript;
    private bool countdown;
    public GameObject gameOverPanel;

    // Update is called once per frame

    void Start()
    {
        countdown = false;
    }

    void Update()
    {
        CheckGameOverPanel(); // Vérifie si le panneau est actif et stoppe le temps si besoin

        if(countdown)
        {
            return;
        }

        if(remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        else if (remainingTime < 0)
        {
            remainingTime = 0;                  // Si le temps restant arrive à 0, alors il reste à 0
            timerText.color = Color.red;
            gameScript.GameOver();
            countdown = true;

        }
        timerText.text = GetDigitTime(remainingTime);
    }

    public void ResetTimer()
    {
        remainingTime = 120f;
        countdown = false;
        timerText.color = Color.white;
    }

    private void CheckGameOverPanel()
    {
        if (gameOverPanel.activeSelf)
        {
            Time.timeScale = 0; // Arrête le temps si le GameOverPanel est actif
        }
    }

    public static string GetDigitTime(float remainingTime)
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
