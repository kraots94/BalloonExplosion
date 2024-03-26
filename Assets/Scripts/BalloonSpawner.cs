using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Balloon balloonPrefab;

    private int timeToNextSpawn;
    private float timer;
    private bool canSpawn;

    public void EnableSpawner(int timeToSpawn)
    {
        timeToNextSpawn = timeToSpawn;
        canSpawn = true;
    }

    public void DisableSpawner()
    {
        canSpawn = false;
    }

    private void Update()
    {
        if (!canSpawn) return;

        timer += Time.deltaTime;
        if (timer > timeToNextSpawn)
        {
            SpawnBalloon();
            timer = 0;
        }
    }

    private void SpawnBalloon()
    {
        int index = UnityEngine.Random.Range(0, spawnPoints.Count);
        Transform spawnTransformPoint = spawnPoints[index];
        Balloon spawnedBalloon = Instantiate(balloonPrefab, spawnTransformPoint.position, Quaternion.identity);
        spawnedBalloon.Init();
    }
}
