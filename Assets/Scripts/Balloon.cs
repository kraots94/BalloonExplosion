using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Balloon : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Renderer balloonRenderer;
    [SerializeField] private Transform balloonTransform;
    [SerializeField] private AudioClip popEffect;
    [SerializeField] private AudioClip inflatingEffect;
    [SerializeField] private ParticleSystem explosionSystem;
    
    private Color _myColor;

    private bool _isDestroying;

    public void BalloonHit()
    {
        if (_isDestroying) return;
        GameManager.Instance.AddPoints();
        DestroyBalloon();
    }

    public void DestroyBalloon()
    {
        if (_isDestroying) return;
        _isDestroying = true;
        StartCoroutine(DestroyBalloonAnimation());
    }

    IEnumerator DestroyBalloonAnimation()
    {
        PlayAudioClip(popEffect);
        balloonRenderer.enabled = false;
        var mainPS = explosionSystem.main;
        mainPS.startColor = _myColor;
        explosionSystem.Play();
        yield return new WaitForSeconds(popEffect.length);

        Destroy(gameObject);
    }

    IEnumerator InflateBalloon()
    {
        PlayAudioClip(inflatingEffect);

        float soundLength = inflatingEffect.length;
        int stepsPerSeconds = 30; // assuming 30fps 
        int totalSteps = Mathf.FloorToInt(soundLength * stepsPerSeconds);
        float minScale = 1;
        float maxScale = 7;

        for (float s = 0; s < totalSteps; s++)
        {
            float scaledVal = Mathf.Lerp(minScale, maxScale, s / totalSteps);
            balloonTransform.localScale = new Vector3(scaledVal, scaledVal, scaledVal);
            yield return new WaitForSeconds(1f / (float) stepsPerSeconds);
        }

        balloonTransform.localScale = new Vector3(maxScale, maxScale, maxScale);
    }


    public void Init()
    {
        _isDestroying = false;
        SetRandomColor();
        StartCoroutine(InflateBalloon());
    }

    private void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void SetRandomColor()
    {
        _myColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        balloonRenderer.material.color = _myColor;
    }
}
