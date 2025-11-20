using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] private AudioSource audioSource;   // For jumpscare sound
    [SerializeField] private AudioClip jumpscareSound;  // The jumpscare audio

    [Header("Jumpscare UI")]
    [SerializeField] private GameObject jumpscareImage; // UI image for jumpscare
    [SerializeField] private float jumpscareDuration = 2f; // How long to show it

    private bool hasCollided = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Subscribe to GameManager play event
        GameManager.instance.onPlay.AddListener(ActivatePlayer);

        // Make sure jumpscare image is hidden at start
        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);
    }

    private void ActivatePlayer()
    {
        // Reactivate player when Play or Replay is clicked
        gameObject.SetActive(true);
        hasCollided = false;

        // Ensure jumpscare is hidden
        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);

        // Reset physics if needed
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasCollided) return;

        if (other.transform.CompareTag("Obstacle"))
        {
            hasCollided = true;

            // Freeze the player instead of disabling
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic; 
            }

            // Play jumpscare sound
            if (audioSource != null && jumpscareSound != null)
                audioSource.PlayOneShot(jumpscareSound);

            // Show jumpscare image
            if (jumpscareImage != null)
                jumpscareImage.SetActive(true);

            // Wait then hide jumpscare & show Game Over UI
            StartCoroutine(HandleJumpscare());
        }
    }

    private IEnumerator HandleJumpscare()
    {
        yield return new WaitForSeconds(jumpscareDuration);

        // Hide jumpscare image
        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);

        // Show Game Over UI
        GameManager.instance.GameOver();

        // Finally deactivate player AFTER coroutine
        gameObject.SetActive(false);
    }
}
