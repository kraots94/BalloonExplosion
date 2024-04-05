using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerHand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject hitGameObject = other.transform.gameObject;
        Balloon balloon = hitGameObject.GetComponentInParent<Balloon>();
        if (balloon != null)
        {
            balloon.BalloonHit();
        }
    }
}
