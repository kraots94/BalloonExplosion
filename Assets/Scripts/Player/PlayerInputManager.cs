using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    [SerializeField] InputActionReference pressPositionReference;
    [SerializeField] Player player;

    private AttitudeSensor attitudeSensor = null;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        attitudeSensor = AttitudeSensor.current;
        if (attitudeSensor != null)
        {
            if (!attitudeSensor.enabled)
            {
                InputSystem.EnableDevice(attitudeSensor);
            }
        }
    }

    public void OnPlayerTouch(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.action.phase);
        if (ctx.action.phase == InputActionPhase.Started)
        {
            var position = pressPositionReference.action.ReadValue<Vector2>();
            player.PlayerTouchedScreen(position);
        }
    }

    public bool IsGyroEnabled()
    {
        return attitudeSensor != null;
    }

    public Quaternion GetGyroAttitude()
    {
        if (attitudeSensor != null)
        {
            return attitudeSensor.attitude.ReadValue();
        }
        else
        {
            return Quaternion.identity;
        }
    }

}
