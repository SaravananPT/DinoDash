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

    [Header("Game Over")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    public Button reviveButton;
    public Button restartButton;

    private bool hasRevived = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        ShowHomeUI();
        reviveButton.onClick.AddListener(OnReviveClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
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
        finalScoreText.text = "Score: " + Mathf.FloorToInt(ScoreManager.Instance.GetScore()).ToString();
        highScoreText.text = "Best: " + PlayerPrefs.GetFloat("HighScore", 0).ToString("F0");
        reviveButton.gameObject.SetActive(!hasRevived);
    }

    private void OnReviveClicked()
    {
        AdManager.Instance.ShowRewarded(() =>
        {
            hasRevived = true;
            reviveButton.gameObject.SetActive(false);
            GameManager.Instance.ReviveGame();
            FindObjectOfType<DinoController>().Revive();
            gameOverPanel.SetActive(false);
        });
    }

    private void OnRestartClicked()
    {
        hasRevived = false;
        GameManager.Instance.RestartGame();
    }

    public void OnStartClicked()
    {
        GameManager.Instance.StartGame();
    }
}
