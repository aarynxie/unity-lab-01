using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionManager : MonoBehaviour
{
    public UnityEvent jump;
    public UnityEvent jumpHold;
    public UnityEvent<int> moveCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnJumpHoldAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("JumpHold was performed");
            Debug.Log(context.duration);
            jumpHold.Invoke();
        }
    }

    public void OnJumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
            jump.Invoke();
    }

    public void OnMoveAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Move started");
            int faceRight = context.ReadValue<float>() > 0 ? 1 : -1;
            moveCheck.Invoke(faceRight);
        }
        else if (context.canceled)
        {
            // stop movement
            moveCheck.Invoke(0);
        }
    }

    public void OnClickAction(InputAction.CallbackContext context)
    {
        if (context.started)
            Debug.Log("mouse click started");
        else if (context.performed)
            Debug.Log("mouse click performed");
        else if (context.canceled)
            Debug.Log("mosue click cancelled");
    }

    public void OnPointerAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 point = context.ReadValue<Vector2>();
            Debug.Log($"Point detection: {point}");
        }
    }
}
