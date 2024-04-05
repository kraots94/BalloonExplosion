using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    public void ResetUI()
    {
        scoreText.text = "0";
        timeText.text = "00:00";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateTime(float time)
    {
        int minutes = Mathf.FloorToInt(time) / 60;
        int seconds = Mathf.FloorToInt(time) % 60;

        string timeString = "";
        timeString += minutes > 9 ? minutes.ToString() : "0" + minutes.ToString();
        timeString += ":";
        timeString += seconds > 9 ? seconds.ToString() : "0" + seconds.ToString();

        timeText.text = timeString;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
