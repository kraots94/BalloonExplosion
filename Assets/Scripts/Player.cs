using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] InputActionReference pressPositionReference;
    [SerializeField] Camera mainCamera;

    public void OnPlayerTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.action.phase);
        if(ctx.action.phase == InputActionPhase.Started)
        {
            var position = pressPositionReference.action.ReadValue<Vector2>();
            Debug.Log($"Position: {position}");

            // Creo un raggio
            Ray ray = mainCamera.ScreenPointToRay(position);

            // Se il raggio colpisce qualcosa
            if (Physics.Raycast(ray, out RaycastHit hit, 100, ~0 , QueryTriggerInteraction.UseGlobal))
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
}
