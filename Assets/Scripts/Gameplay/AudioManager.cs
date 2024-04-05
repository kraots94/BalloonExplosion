using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource uiEffectsAudioSource;
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioClip uiClickEffect;
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayBackroundMusic();
    }

    private void PlayBackroundMusic()
    {
        backgroundMusicAudioSource.clip = backgroundMusic;
        backgroundMusicAudioSource.loop = true;
        backgroundMusicAudioSource.Play();
    }

    public void PlayHitEffect()
    {
        uiEffectsAudioSource.Stop();
        uiEffectsAudioSource.clip = uiClickEffect;
        uiEffectsAudioSource.loop = false;
        uiEffectsAudioSource.Play();
    }
}