using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string highscoreLabelPlayerPrefs = "playerHighscore";
    public static GameManager Instance { get; private set; }

    [Tooltip("Tempo in secondi di gioco della partita")]
    [SerializeField, Range(15, 120)] private int gameplayTime;

    [Tooltip("Tempo in secondi tra la generazione di un palloncino e il successivo")]
    [SerializeField, Range(1, 10)] private int timeToNextBaloon;

    [Tooltip("Punti ricevuti per ogni palloncino scoppiato")]
    [SerializeField, Range(5, 15)] private int pointsGain;

    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameOverUI gameOverUI;

    [SerializeField] private BalloonSpawner ballonSpawner;

    private enum GameState
    {
        WaitToPlay,
        Playing,
        EndGame
    }

    private GameState _currentState;
    private int points;
    private float timer;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        int currentHighScore = PlayerPrefs.GetInt(highscoreLabelPlayerPrefs, 0);
        mainMenuUI.UpdateUI(currentHighScore);
        UpdateState(GameState.WaitToPlay);
    }

    public void StartGame()
    {
        points = 0;
        timer = gameplayTime;
        gameUI.UpdateScore(points);
        ballonSpawner.EnableSpawner(timeToNextBaloon);

        UpdateState(GameState.Playing);
    }

    public void RestartGame()
    {
        StartGame();
    }

    private void GameOver()
    {
        DestroyAllBalloons();
        ballonSpawner.DisableSpawner();

        // handling highscore
        int currentHighScore = PlayerPrefs.GetInt(highscoreLabelPlayerPrefs, 0);
        bool newHighScore = points > currentHighScore;
        if (newHighScore)
        {
            // saving new highscore
            PlayerPrefs.SetInt(highscoreLabelPlayerPrefs, points);
            PlayerPrefs.Save();
        }

        gameOverUI.UpdateUI(points, newHighScore);
        UpdateState(GameState.EndGame);
    }

    void Update()
    {
        switch (_currentState)
        {
            case GameState.WaitToPlay:
                break;
            case GameState.Playing:
                timer -= Time.deltaTime;

                if (timer < 0)
                {
                    GameOver();
                }

                gameUI.UpdateTime(timer);
                break;
            case GameState.EndGame:
                break;
        }
    }

    public void BalloonPop()
    {
        points += pointsGain;
        gameUI.UpdateScore(points);
    }

    public void DestroyAllBalloons()
    {
        Balloon[] balloons = GameObject.FindObjectsOfType<Balloon>();
        foreach (Balloon balloon in balloons)
        {
            balloon.DestroyBalloon();
        }
    }

    private void UpdateState(GameState newState)
    {
        if (_currentState == newState)
        {
            Debug.LogWarning($"Updating game state with same state: [{newState}]");
        }
        _currentState = newState;

        // Mostrare l'interfaccia corretta
        switch (_currentState)
        {
            case GameState.WaitToPlay:
                mainMenuUI.Show();
                gameUI.Hide();
                gameOverUI.Hide();
                break;
            case GameState.Playing:
                mainMenuUI.Hide();
                gameUI.Show();
                gameOverUI.Hide();
                break;
            case GameState.EndGame:
                mainMenuUI.Hide();
                gameUI.Hide();
                gameOverUI.Show();
                break;
        }
    }
}
