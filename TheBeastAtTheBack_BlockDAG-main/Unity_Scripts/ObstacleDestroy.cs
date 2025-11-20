using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    [Header("Optional FX")]
    public GameObject sceneParticleObject;  
    public AudioClip breakSound;
    public float destroyDelay = 0.05f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wolf"))
        {
            BreakObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wolf"))
        {
            BreakObstacle();
        }
    }

    void BreakObstacle()
    {
        // Move the particle GameObject & play it
        if (sceneParticleObject != null)
        {
            sceneParticleObject.transform.position = transform.position;

            // Try to play the particle system on it
            ParticleSystem ps = sceneParticleObject.GetComponent<ParticleSystem>();
            if (ps != null) ps.Play();
        }

        // Play sound
        if (breakSound != null && audioSource != null)
            audioSource.PlayOneShot(breakSound);

        // Destroy this obstacle
        Destroy(gameObject, destroyDelay);
    }
}
