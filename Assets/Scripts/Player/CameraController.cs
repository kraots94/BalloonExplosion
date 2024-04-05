using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Tooltip("Posizione nel mondo del gioco quando non viene supportato il giroscopio")]
    [SerializeField] Transform positionNoGyro;
    [Tooltip("Posizione nel mondo del gioco quando viene supportato il giroscopio")]
    [SerializeField] Transform positionGyro;
    private bool firstUpdate;

    private void Start()
    {
        firstUpdate = true;
    }

    private void Update()
    {
        if (firstUpdate)
        {
            if(PlayerInputManager.Instance.IsGyroEnabled())
            {
                transform.position = positionGyro.position;
                transform.rotation = positionGyro.rotation;
            }
            else
            {
                transform.position = positionNoGyro.position;
                transform.rotation = positionNoGyro.rotation;
            }

            firstUpdate = false;
        }

        if (PlayerInputManager.Instance.IsGyroEnabled())
        {
            var attitudeValue = PlayerInputManager.Instance.GetGyroAttitude();
            transform.rotation = Quaternion.Euler(90, 0, 0) * GyroToUnity(attitudeValue);
        }
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
