using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadePlayerMovement : MonoBehaviour
{
    public bool squashAndStrech;
    public Animator animator;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public bool isGrounded;
    public bool isCrouching;
    public float jumpForce;
    public float speed;
    Rigidbody2D rb;

    private float jumpBuffer;
    private float coyoteTime;
    private float jumpCoolDown;

    private Transform spriteHolder;

    void Start()
    {
        spriteHolder = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        Vector2 input = InputManager.Instance.GetPlayerMovement();

        if(input.x < 0)
        {
            spriteHolder.localScale = new Vector3(-1, 1, 1);
        }else if(input.x > 0)
        {
            spriteHolder.localScale = new Vector3(1, 1, 1);
        }

        if (InputManager.Instance.WasSpacePressed())
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
            rb.gravityScale = 3f;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            if(squashAndStrech)
                StartCoroutine(JumpSqueeze(0.9f, 1.1f, 0.1f));
        }

        if((!isGrounded && !InputManager.Instance.IsSpacePressed()) || rb.linearVelocity.y < 0)
        {
            rb.gravityScale = 6;

            if(!isGrounded)
                animator.SetBool("Falling", true);
        }

        bool wasCrouching = isCrouching;
        isCrouching = isGrounded && input.y < 0;
        if(isCrouching != wasCrouching && squashAndStrech)
            StartCoroutine(JumpSqueeze(1.1f, 0.9f, 0.1f));
        animator.SetBool("Crouching", isCrouching);

        jumpCoolDown -= Time.deltaTime;
        coyoteTime -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
        float x = InputManager.Instance.GetPlayerMovement().x;
        if (Mathf.Approximately(x, 0) || isCrouching)
            x = -rb.linearVelocity.x / 10;

        Vector3 move = new Vector3(x * speed, rb.linearVelocity.y, 0f);
        rb.AddForce(move);
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
}
