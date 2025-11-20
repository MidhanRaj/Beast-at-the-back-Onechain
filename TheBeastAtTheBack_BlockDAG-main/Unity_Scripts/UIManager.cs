using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;

    [Header("Hint UI")]
    [SerializeField] private TextMeshProUGUI jumpHintText; // TextMeshPro directly (for fading)

    private GameManager gm;
    private bool leaderboardOpened = false;

    private void Awake()
    {
        gm = GameManager.instance;


        gm.onGameOver.RemoveListener(ActivateGameOverUI);
        gm.onGameOver.AddListener(ActivateGameOverUI);
    }

    private void Start()
    {

        startMenuUI.SetActive(true);
        gameOverUI.SetActive(false);


        if (scoreUI != null)
            scoreUI.gameObject.SetActive(false);


        if (jumpHintText != null)
            jumpHintText.gameObject.SetActive(false);
    }

    public void PlayButtonHandler()
    {
        Debug.Log("Play button pressed");


        startMenuUI.SetActive(false);
        gameOverUI.SetActive(false);


        gm.StartGame();

        leaderboardOpened = false; // reset for new game


        if (scoreUI != null)
            scoreUI.gameObject.SetActive(true);


        if (jumpHintText != null)
            StartCoroutine(FadeHintText());
    }

    private IEnumerator FadeHintText()
    {
        jumpHintText.gameObject.SetActive(true);

        // fully visible
        Color c = jumpHintText.color;
        c.a = 1f;
        jumpHintText.color = c;

        // visible for 1 second
        yield return new WaitForSeconds(1f);

        // fade out over 1 second
        float fadeDuration = 1f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            jumpHintText.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        jumpHintText.gameObject.SetActive(false);
    }

    public void ActivateGameOverUI()
    {
        Debug.Log("Activating GameOver UI");


        gameOverUI.SetActive(true);


        if (scoreUI != null)
            scoreUI.gameObject.SetActive(false);

        int finalScore = Mathf.RoundToInt(GameManager.instance.currentScore);
        gameOverScoreUI.text = "Score: " + finalScore;
        gameOverHighscoreUI.text = "Highscore: " + gm.ApproxHighscore();


        if (!leaderboardOpened)
        {
            leaderboardOpened = true;
            StartCoroutine(OpenLeaderboardWithDelay(finalScore));
        }
    }

    private IEnumerator OpenLeaderboardWithDelay(int finalScore)
    {
        yield return new WaitForSeconds(0.8f);
        Application.OpenURL(
            "https://midhanraj.github.io/TheBeastAtTheBack_BlockDAG/Leaderboard_Web/Leaderboard.html?score=" + finalScore
        );
    }

    private void OnGUI()
    {
        // Only update score when playing
        if (gm.isPlaying && scoreUI != null)
        {
            scoreUI.text = gm.ApproxScore();
        }
    }

    public void PopulateLeaderboard(List<string> formattedEntries)
    {
        foreach (var e in formattedEntries)
        {
            Debug.Log("Leaderboard entry: " + e);
        }
    }

    public void OpenLeaderboardPage()
    {
        Application.OpenURL("https://midhanraj.github.io/TheBeastAtTheBack_BlockDAG/Leaderboard_Web/Leaderboard.html");
    }

    public void ExitGame()
    {
        Debug.Log("Exit button pressed");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
