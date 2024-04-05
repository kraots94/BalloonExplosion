using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public void PlayerTouchedScreen(Vector2 position)
    {
        Debug.Log($"Position: {position}");

        // Creo un raggio
        Ray ray = mainCamera.ScreenPointToRay(position);

        // Se il raggio colpisce qualcosa
        if (Physics.Raycast(ray, out RaycastHit hit, 100, ~0, QueryTriggerInteraction.UseGlobal))
        {
            Debug.Log($"HIT: {hit.transform.gameObject.name}");

            GameObject hitGameObject = hit.transform.gameObject;
            Balloon balloon = hitGameObject.GetComponentInParent<Balloon>();

            if (balloon != null)
            {
                balloon.BalloonHit();
            }
        }
    }
}
