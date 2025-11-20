using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPos;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    [Header("Sound Settings")]
    [SerializeField] private AudioSource audioSource;   // Jump sound
    [SerializeField] private AudioClip jumpSound;

    [Header("Animation")]
    [SerializeField] private Animator animator; // Reference to Animator

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    private void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        // Update animator with grounded state
        if (animator != null)
            animator.SetBool("isGrounded", isGrounded);

        // Detect jump input for ALL platforms:
        bool jumpPressed =
            Input.GetKeyDown(KeyCode.Space) ||        // Keyboard
            Input.GetMouseButtonDown(0) ||            // Mobile tap / mouse click
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began); // True touch

        // Initial jump
        if (isGrounded && jumpPressed)
        {
            isJumping = true;
            rb.linearVelocity = Vector2.up * jumpForce;

            // Trigger jump animation
            if (animator != null)
                animator.SetTrigger("Jump");

            // Play jump sound
            if (audioSource != null && jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
        }

        // Hold jump for variable height
        if (isJumping && (Input.GetButton("Jump") || Input.GetMouseButton(0)))
        {
            if (jumpTimer < jumpTime)
            {
                rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Reset jump when released
        if (Input.GetButtonUp("Jump") || Input.GetMouseButtonUp(0))
        {
            isJumping = false;
            jumpTimer = 0;
        }
    }
}
