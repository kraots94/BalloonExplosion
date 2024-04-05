using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance { get; private set; }

    [SerializeField] GameObject PlayerVR;
    [SerializeField] GameObject CanvasVR;
    [SerializeField] GameObject PlayerNVR;
    [SerializeField] GameObject CanvasNVR;

    public enum Platform
    {
        None,
        PC,
        SMARTPHONE,
        VR
    }

    public static Platform platform { get; private set; }

    private bool _vrEnabled;
    private bool _firstUpdate;
    private bool _platformReady;

    public void SetPlatform(bool usingXR)
    {
        _vrEnabled = usingXR;
        if (_vrEnabled)
        {
            platform = Platform.VR;
            PlayerVR.SetActive(true);
            CanvasVR.SetActive(true);
            PlayerNVR.SetActive(false);
            CanvasNVR.SetActive(false);
        }
        else
        {
#if UNITY_ANDROID
            platform = Platform.SMARTPHONE;
#else
            platform = Platform.PC;
#endif
            PlayerNVR.SetActive(true);
            CanvasNVR.SetActive(true);
            PlayerVR.SetActive(false);
            CanvasVR.SetActive(false);
        }
        _platformReady = true;
    }

    private void Awake()
    {
        Instance = this;
        StartCoroutine(ManualXRControl.StartXRCoroutine());
        _platformReady = false;
    }
    
    private void Start()
    {
        _firstUpdate = true;
    }

    private void Update()
    {
        if(_firstUpdate)
        {
            if (_platformReady)
            {
                GameManager.Instance.LinkUI(platform);
                _firstUpdate = false;
            }
        }
    }

    private void OnDestroy()
    {
        if(_vrEnabled)
        {
            ManualXRControl.StopXR();
        }
    }
}
