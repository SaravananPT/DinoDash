using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Home, Playing, Dead }
    public GameState currentState = GameState.Home;

    public float gameSpeed = 8f;
    public float speedIncreaseRate = 0.05f;
    public float maxSpeed = 30f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
            gameSpeed = Mathf.Min(gameSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        UIManager.Instance.ShowGameUI();
    }

    public void DinoHit()
    {
        if (currentState == GameState.Dead) return;
        currentState = GameState.Dead;
        CameraShake.Instance.Shake(0.4f, 0.3f);
        SoundManager.Instance.PlayDeath();
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
        gameSpeed = 8f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
