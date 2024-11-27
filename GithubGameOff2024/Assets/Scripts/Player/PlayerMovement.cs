using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float lookSpeed;
    [SerializeField]
    float gravityValue;

    [Header("Look Controls")]

    CharacterController controller;
    private Vector3 playerVelocity;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Start() {

    }

    void Update()
    {
        //Move();
        Gravity(); //This should be running regardless if player can move
    }

    public void MoveUpdate()
    {
        Move();
        Look();
    }
    
    private void Move() {
        Vector2 movement = InputManager.Instance.GetPlayerMovement();
        Vector3 move = new (movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        controller.Move(moveSpeed * Time.deltaTime * move);
        // if (move != Vector3.zero) {
        //     gameObject.transform.forward = move;
        // }
    }

    private void Gravity()
    {
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Look() {
        transform.Rotate(Vector3.up, lookSpeed * InputManager.Instance.GetLook().x);
    }

    
}
