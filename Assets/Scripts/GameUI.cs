using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text timeLabel;

    public void UpdateScore(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdateTime(float time)
    {
        // Calcolo quanti minuti e secondi 
        int minutes = Mathf.FloorToInt(time) / 60;
        int seconds = Mathf.FloorToInt(time) % 60;

        // Creo la string nel formato mm:ss
        string timeString = "";
        timeString += minutes > 9 ? minutes.ToString() : "0" + minutes.ToString();
        timeString += ":";
        timeString += seconds > 9 ? seconds.ToString() : "0" + seconds.ToString();

        timeLabel.text = timeString;
    }
}
