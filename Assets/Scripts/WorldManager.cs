using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    [Header("World Thresholds")]
    public float iceWorldScore = 500f;
    public float lavaWorldScore = 1500f;

    private int currentWorld = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        float score = ScoreManager.Instance.GetScore();

        if (score >= lavaWorldScore && currentWorld != 2)
            SwitchWorld(2);
        else if (score >= iceWorldScore && currentWorld != 1 && score < lavaWorldScore)
            SwitchWorld(1);
    }

    private void SwitchWorld(int worldIndex)
    {
        currentWorld = worldIndex;
        SoundManager.Instance.PlayWorldMusic(worldIndex);
        RenderSettings.fogColor = worldIndex == 2
            ? new Color(0.6f, 0.1f, 0.05f)
            : worldIndex == 1
                ? new Color(0.7f, 0.85f, 1f)
                : new Color(0.53f, 0.81f, 0.92f);
    }

    public int GetCurrentWorld() => currentWorld;
}
