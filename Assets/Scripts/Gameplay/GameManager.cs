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
    [Space(30)]
    [SerializeField] private MainMenuUI nonImmersiveMainMenuUI;
    [SerializeField] private GameUI nonImmersiveGameUI;
    [SerializeField] private GameOverUI nonImmersiveGameOverUI;

    [Space(30)]
    [SerializeField] private MainMenuUI immersiveMainMenuUI;
    [SerializeField] private GameUI immersiveGameUI;
    [SerializeField] private GameOverUI immersiveGameOverUI;

    [Space(30)]
    [SerializeField] private BalloonSpawner ballonSpawner;

    private MainMenuUI _activeMainMenuUI;
    private GameUI _activeGameUI;
    private GameOverUI _activeGameOverUI;

    private enum GameState
    {
        Loading,
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
        UpdateState(GameState.Loading);
    }

    public void LinkUI(PlatformManager.Platform platform) 
    {
        switch (platform)
        {
            case PlatformManager.Platform.None:
            case PlatformManager.Platform.PC:
            case PlatformManager.Platform.SMARTPHONE:
                _activeMainMenuUI = nonImmersiveMainMenuUI;
                _activeGameUI = nonImmersiveGameUI;
                _activeGameOverUI = nonImmersiveGameOverUI;
                break;
            case PlatformManager.Platform.VR:
                _activeMainMenuUI = immersiveMainMenuUI;
                _activeGameUI = immersiveGameUI;
                _activeGameOverUI = immersiveGameOverUI;
                break;
        }

        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        int currentHighScore = PlayerPrefs.GetInt(highscoreLabelPlayerPrefs, 0);
        _activeMainMenuUI.UpdateUI(currentHighScore);
        UpdateState(GameState.WaitToPlay);
    }

    public void StartGame()
    {
        points = 0;
        timer = gameplayTime;
        _activeGameUI.UpdateScore(points);
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

        _activeGameOverUI.UpdateUI(points, newHighScore);
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

                _activeGameUI.UpdateTime(timer);
                break;
            case GameState.EndGame:
                break;
        }
    }

    public void BalloonPop()
    {
        points += pointsGain;
        _activeGameUI.UpdateScore(points);
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
            case GameState.Loading:
                break;
            case GameState.WaitToPlay:
                _activeMainMenuUI.Show();
                _activeGameUI.Hide();
                _activeGameOverUI.Hide();
                break;
            case GameState.Playing:
                _activeMainMenuUI.Hide();
                _activeGameUI.Show();
                _activeGameOverUI.Hide();
                break;
            case GameState.EndGame:
                _activeMainMenuUI.Hide();
                _activeGameUI.Hide();
                _activeGameOverUI.Show();
                break;
            default:
                break;
        }
    }
}
