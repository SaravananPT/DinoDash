using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject homePanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;

    [Header("Game HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinText;

    [Header("Game Over")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalHighScoreText;
    public TextMeshProUGUI finalCoinsText;
    public Button reviveButton;
    public Button restartButton;
    public Button homeButton;

    [Header("Home")]
    public TextMeshProUGUI homeHighScoreText;
    public Button playButton;
    public Button skinsButton;

    private bool hasRevived = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        ShowHomeUI();
        playButton.onClick.AddListener(() => GameManager.Instance.StartGame());
        reviveButton.onClick.AddListener(OnReviveClicked);
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        homeButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        homeHighScoreText.text = "BEST: " + Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore", 0)).ToString();
    }

    public void ShowHomeUI()
    {
        homePanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameUI()
    {
        homePanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = Mathf.FloorToInt(ScoreManager.Instance.GetScore()).ToString();
        finalHighScoreText.text = "BEST  " + Mathf.FloorToInt(ScoreManager.Instance.GetHighScore()).ToString();
        finalCoinsText.text = "🪙 " + ScoreManager.Instance.GetCoins().ToString();
        reviveButton.gameObject.SetActive(!hasRevived);
    }

    private void OnReviveClicked()
    {
        AdManager.Instance.ShowRewarded(() =>
        {
            hasRevived = true;
            reviveButton.gameObject.SetActive(false);
            gameOverPanel.SetActive(false);
            GameManager.Instance.ReviveGame();
            FindObjectOfType<DinoController>().Revive();
        });
    }
}
