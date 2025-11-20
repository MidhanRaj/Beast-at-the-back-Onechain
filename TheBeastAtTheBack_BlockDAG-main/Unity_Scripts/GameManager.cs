using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float currentScore = 0f;
    public SaveData data;
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;  // Score increases only when playing
        }
    }

    public void StartGame()
    {
        Debug.Log(">>> StartGame called");
        isPlaying = true;
        currentScore = 0;
        onPlay.Invoke();
    }

    public void GameOver()
    {
        Debug.Log(">>> GameOver called");

        // Save highscore if beaten
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
            string saveString = JsonUtility.ToJson(data);
            SaveSystem.Save("save", saveString);
        }

        isPlaying = false;
        onGameOver.Invoke();
    }

    public string ApproxScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string ApproxHighscore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }

    private void Start()
    {
        // Load existing save
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<SaveData>(loadedData);
        }
        else
        {
            data = new SaveData();
        }
    }
}
