using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject newHighScoreLabel;

    public void UpdateUI(int score, bool newHighScore)
    {
        scoreText.text = score.ToString();

        if (newHighScore)
        {
            newHighScoreLabel.gameObject.SetActive(true);
        }
        else
        {
            newHighScoreLabel.gameObject.SetActive(false);
        }
    }

    public void PlayAgain()
    {
        GameManager.Instance.RestartGame();
    }

    public void ShowMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
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
