using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set;}

    private InputSystem_Actions inputActions;

     private void Awake() {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        inputActions = new();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    public Vector2 GetPlayerMovement() {
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool IsInteractButtonPressed() {
        float val = inputActions.Player.Interact.ReadValue<float>();
        return val == 1.0f;
    }

    public bool WasInteractButtonPressed()
    {
        return inputActions.Player.Interact.WasPressedThisFrame();
    }

    public bool WasSpacePressed()
    {
        return inputActions.Player.Jump.WasPressedThisFrame();
    }
}
