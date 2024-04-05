using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public static class ManualXRControl
{
    public static bool UsingXR = false;

    public static IEnumerator StartXRCoroutine()
    {
        Debug.Log("Initializing XR...");

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            yield return XRGeneralSettings.Instance.Manager.InitializeLoader();
        }

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogWarning("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.automaticRunning = false;
            XRGeneralSettings.Instance.Manager.StartSubsystems();
            UsingXR = true;
        }

        PlatformManager.Instance.SetPlatform(UsingXR);
    }

    public static void StopXR()
    {
        if (!UsingXR) return;

        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
}