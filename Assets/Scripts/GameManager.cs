using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Tooltip("Tempo in secondi tra la generazione di un palloncino e il successivo")]
    [SerializeField, Range(1, 10)] private int timeToNextBaloon;

    [Tooltip("Punti ricevuti per ogni palloncino scoppiato")]
    [SerializeField, Range(5, 15)] private int pointsGain;

    [SerializeField] private GameUI gameUI;

    [SerializeField] private BalloonSpawner ballonSpawner;

    private int points;
    private float timer;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Too many Instances!");
        }

        Instance = this;
    }

    private void Start()
    {
        timer = 0;
        points = 0;
        gameUI.UpdateTime(timer);
        gameUI.UpdateScore(points);
        ballonSpawner.EnableSpawner(timeToNextBaloon);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        gameUI.UpdateTime(timer);
    }

    public void AddPoints()
    {
        points = points + pointsGain;
        gameUI.UpdateScore(points);
    }
}
