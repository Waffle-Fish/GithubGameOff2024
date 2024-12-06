using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcadePlatformer
{
    public class ArcadePlayerMovement : MonoBehaviour
    {
        public bool squashAndStrech;
        public Animator animator;
        public LayerMask whatIsGround;
        public Transform groundCheck;
        public bool isGrounded;
        public bool isCrouching;
        public float jumpForce;
        public float groundCheckDistance;
        public float speed;
        public float maxSpeed;
        public float maxSpeedReduction = 0.1f;
        Rigidbody2D rb;

        private float jumpBuffer;
        private float coyoteTime;
        private float jumpCoolDown;

        private Transform spriteHolder;

        private Vector2 input;
        private bool jumpWasPressed;
        private bool jumpIsPressed;

        public ArcadeManager manager;

        private float randomSwitchMovementTimer;
        private float randomJumpTimer;
        private float letGoOfSpaceTimer;

        private float normalGrav = 3, fallingGrav = 8;

        private ArcadePlayer player;

        void Start()
        {
            player = GetComponent<ArcadePlayer>();
            spriteHolder = transform.GetChild(0);
            rb = GetComponent<Rigidbody2D>();
        }

        void GetInput()
        {
            input = InputManager.Instance.GetPlayerMovement();
            jumpWasPressed = InputManager.Instance.WasSpacePressed();
            jumpIsPressed = InputManager.Instance.IsSpacePressed();

            if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
                player.Die();
        }

        void NPCInput()
        {
            if (randomSwitchMovementTimer < 0)
            {
                randomSwitchMovementTimer = Random.Range(0.1f, 1.4f);
                input.x = Random.Range(-1, 2);
                input.y = Random.Range(-1, 5);
            }

            if (randomJumpTimer < 0)
            {
                jumpWasPressed = true;
                jumpIsPressed = true;
                randomJumpTimer = Random.Range(0.2f, 2f);
                letGoOfSpaceTimer = Random.Range(0.1f, 0.5f);
            }

            if (letGoOfSpaceTimer < 0)
            {
                jumpWasPressed = false;
                jumpIsPressed = false;
            }

            randomSwitchMovementTimer -= Time.deltaTime;
            randomJumpTimer -= Time.deltaTime;
            letGoOfSpaceTimer -= Time.deltaTime;
        }

        void Update()
        {
            if (manager.playerOperated)
                GetInput();
            else
                NPCInput();

            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

            if (input.x < 0)
            {
                spriteHolder.localScale = new Vector3(-1, 1, 1);
            }
            else if (input.x > 0)
            {
                spriteHolder.localScale = new Vector3(1, 1, 1);
            }

            if (jumpWasPressed)
                jumpBuffer = 0.1f;

            if (isGrounded)
            {
                coyoteTime = 0.1f;
                animator.SetBool("Falling", false);
            }

            if (jumpBuffer > 0 && coyoteTime > 0 && jumpCoolDown < 0)
            {
                jumpCoolDown = 0.2f;
                coyoteTime = 0;
                jumpBuffer = 0;
                rb.gravityScale = normalGrav;

                if (rb.linearVelocity.y < 0)
                    rb.linearVelocity = Vector2.right * rb.linearVelocity;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;

                if (squashAndStrech)
                    StartCoroutine(JumpSqueeze(0.9f, 1.1f, 0.1f));
            }

            if (!isGrounded && !jumpIsPressed || rb.linearVelocity.y < 0 && !isGrounded)
            {
                rb.gravityScale = fallingGrav;

                if (!isGrounded)
                    animator.SetBool("Falling", true);
            }
            else if (isGrounded && jumpCoolDown < 0)
                rb.gravityScale = 1;

            bool wasCrouching = isCrouching;
            isCrouching = isGrounded && input.y < 0;
            if (isCrouching != wasCrouching && squashAndStrech)
                StartCoroutine(JumpSqueeze(1.1f, 0.9f, 0.1f));
            animator.SetBool("Crouching", isCrouching);

            jumpCoolDown -= Time.deltaTime;
            coyoteTime -= Time.deltaTime;
            jumpBuffer -= Time.deltaTime;
        }

        void FixedUpdate()
        {
            RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);
            if (!hit2D || whatIsGround != (whatIsGround | (1 << hit2D.collider.gameObject.layer)))
                hit2D = Physics2D.Raycast(groundCheck.position + Vector3.right * 0.4f, Vector2.down, groundCheckDistance);
            else if (!hit2D || whatIsGround != (whatIsGround | (1 << hit2D.collider.gameObject.layer)))
                hit2D = Physics2D.Raycast(groundCheck.position + Vector3.left * 0.4f, Vector2.down, groundCheckDistance);

            isGrounded = hit2D;
            if (hit2D && whatIsGround != (whatIsGround | (1 << hit2D.collider.gameObject.layer)))
                isGrounded = false;

            //isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
            float x = input.x;
            if ((Mathf.Approximately(x, 0) || isCrouching)) {
                if(isGrounded)
                    x = -rb.linearVelocity.x / 10; }
            else if (x > 0 != rb.linearVelocity.x > 0)
                x *= 2;

            if (Mathf.Abs(rb.linearVelocity.x) > maxSpeed && rb.linearVelocity.x > 0 == x > 0)
                x *= maxSpeedReduction;

            Vector3 move = new Vector3(x * speed, 0, 0f);
            rb.AddForce(move);
        }

        public void SpringJump(float power)
        {
            jumpCoolDown = 0.2f;
            coyoteTime = 0;
            jumpBuffer = 0;
            rb.gravityScale = fallingGrav;

            rb.linearVelocity = rb.linearVelocity * Vector2.right;
            rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            isGrounded = false;

            if (squashAndStrech)
                StartCoroutine(JumpSqueeze(0.9f, 1.1f, 0.1f));
        }

        IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
        {
            Vector3 originalSize = Vector3.one;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
            float t = 0f;
            while (t <= 1.0)
            {
                t += Time.deltaTime / seconds;
                animator.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
                yield return null;
            }
            t = 0f;
            while (t <= 1.0)
            {
                t += Time.deltaTime / seconds;
                animator.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
                yield return null;
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position - Vector3.up * groundCheckDistance);
            Gizmos.DrawLine(transform.position + Vector3.right * 0.4f, transform.position + Vector3.right * 0.4f - Vector3.up * groundCheckDistance);
            Gizmos.DrawLine(transform.position + Vector3.left * 0.4f, transform.position + Vector3.left * 0.4f - Vector3.up * groundCheckDistance);
        }
    }
}