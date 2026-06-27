using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Home, Playing, Dead }
    public GameState currentState = GameState.Home;

    public float gameSpeed = 5f;
    public float speedIncreaseRate = 0.1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            gameSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        UIManager.Instance.ShowGameUI();
    }

    public void DinoHit()
    {
        currentState = GameState.Dead;
        UIManager.Instance.ShowGameOverUI();
        AdManager.Instance.ShowInterstitial();
    }

    public void ReviveGame()
    {
        currentState = GameState.Playing;
        UIManager.Instance.ShowGameUI();
    }

    public void RestartGame()
    {
        gameSpeed = 5f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
