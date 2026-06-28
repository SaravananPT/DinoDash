using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private float score;
    private float highScore;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        score += GameManager.Instance.gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            highScoreText.text = "BEST " + Mathf.FloorToInt(highScore).ToString();
        }
    }

    private int coins;
    public void AddCoin() { coins += 10; score += 10; }
    public int GetCoins() => coins;
    public void ResetScore() { score = 0; coins = 0; }
    public float GetScore() => score;
    public float GetHighScore() => highScore;
}
