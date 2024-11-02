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
        Debug.Log(inputActions.Player.Move.ReadValue<Vector2>());
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta() {
        Debug.Log(inputActions.Player.Look.ReadValue<Vector2>());
        return inputActions.Player.Look.ReadValue<Vector2>();
    }
}
