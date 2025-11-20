using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [Header("Speed Settings")]
    public float baseGameSpeed = 2f;          // starting speed when playing
    public float rampRate = 0.05f;            // how fast it ramps up each second
    public float menuSpeed = 0.5f;            // constant slow speed for menu/game over

    private float width;
    private float currentSpeed;
    private float elapsedPlayTime;

    void Start()
    {
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        currentSpeed = menuSpeed;

        GameManager.instance.onPlay.AddListener(OnGameStart);
        GameManager.instance.onGameOver.AddListener(OnGameOver);
    }

    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            elapsedPlayTime += Time.deltaTime;
            currentSpeed = baseGameSpeed + (elapsedPlayTime * rampRate);
        }
        else
        {
            currentSpeed = menuSpeed;
        }

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < -width)
        {
            transform.position += Vector3.right * width * 2f;
        }
    }

    private void OnGameStart()
    {
        elapsedPlayTime = 0f;
        currentSpeed = baseGameSpeed;
    }

    private void OnGameOver()
    {
        currentSpeed = menuSpeed;
        elapsedPlayTime = 0f;
    }
}
