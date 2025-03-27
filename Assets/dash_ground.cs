using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    // Dash parameters
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;

    // Flag to check if the player can dash
    private bool canDash = true;

    // The current dash direction
    private Vector2 dashDirection;

    // Rigidbody reference
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check for dash input only if dash is available
        if ((Input.GetKeyDown(KeyCode.UpArrow) && canDash))
        {
            // Read input axes to determine dash direction
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // If no directional input is provided, default to the object's right direction
            if (dashDirection == Vector2.zero)
            {
                dashDirection = transform.right;
            }

            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        // Prevent additional dashes until reset on ground contact
        canDash = false;

        // Optionally disable gravity for a more controlled dash trajectory
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            // Apply velocity in the calculated dash direction
            rb.linearVelocity = dashDirection * dashSpeed;
            yield return null;
        }

        // Reset velocity and restore gravity after the dash
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = originalGravity;
    }

    // When the player collides with an object tagged "Ground", allow dashing again
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canDash = true;
        }
    }
}

