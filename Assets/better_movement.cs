using System.Collections;
using UnityEngine;

public class PlatformControllerAdv : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3f;
    private float inputX;
    bool facingRight = true;

    [Header("Ground detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer = (1 << 6);
    [SerializeField] private bool isGrounded;

    [Header("Extended Jumping")]
    [SerializeField] float jumpForce = 5f;
    bool isJumping;
    [SerializeField] float jumpTime = 0.5f;
    float jumpTimeLeft;

    [Header("Coyote time and jumpbuffer")]
    [SerializeField] private float coyoteTime = 0.2f;
    float coyoteTimeLeft;
    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter;

    private Animator anim;
    private bool useAnim = true;
    bool jump = false;

    [Header("Wall Sliding")]
    [SerializeField] bool isWallSliding;
    [SerializeField] float wallSlidingSpeed = 2f;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer = (1 << 7);
    [SerializeField] private Vector2 wallCheckRadius = new Vector2(0.2f, 1.6f);

    [Header("Wall Jumping")]
    bool isWallJumping;
    float wallJumpDir;
    float wallJumpingTime = 0.2f;
    float wallJumpingCounter;
    float wallJumpingDuration = 0.2f;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(4f, 8f);

    [Header("Dashing")]
    bool canDash = true;
    [SerializeField] bool isDashing = false;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (TryGetComponent<Animator>(out Animator a))
        {
            anim = a;
        }
        else
        {
            useAnim = false;
        }
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        // Jump input (W or UpArrow)
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            jump = false;
        }

        // Dash input (Spacebar)
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isWallJumping)
        {
            MovingPlayer();
        }

        WallSlide();
        WallJump();
        PlayerJump();
    }

    private void MovingPlayer()
    {
        if (useAnim)
        {
            anim.SetFloat("Speed", Mathf.Abs(inputX));
        }

        CheckDirection();

        rb.linearVelocity = new Vector2(inputX * speed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (rb.linearVelocity.y < 0f)
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * 1.5f * Time.fixedDeltaTime);
        }
    }

    private void PlayerJump()
    {
        if (isGrounded)
        {
            coyoteTimeLeft = coyoteTime;
        }
        else
        {
            coyoteTimeLeft -= Time.deltaTime;
        }

        if (jump)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeLeft > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
            jumpTimeLeft = jumpTime;
            coyoteTimeLeft = 0;
            jumpBufferCounter = 0;
        }

        if (jump && isJumping)
        {
            if (jumpTimeLeft > 0)
            {
                jumpTimeLeft -= Time.deltaTime;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }

        if (!jump)
        {
            isJumping = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDir = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (jump && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDir * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if (transform.localScale.x != wallJumpDir)
            {
                Flip();
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool isWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, wallCheckRadius, 0f, wallLayer);
    }

    private void WallSlide()
    {
        if (isWalled() && !isGrounded && inputX != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(0, Mathf.Clamp(rb.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void CheckDirection()
    {
        if (inputX < 0 && facingRight)
        {
            Flip();
        }
        if (inputX > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }
}

