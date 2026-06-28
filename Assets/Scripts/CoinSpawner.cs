using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float[] lanePositions = { -2.5f, 0f, 2.5f };
    public float spawnDistance = 50f;
    public float coinHeight = 1.2f;
    public float spawnInterval = 3f;

    private float timer;

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCoinRow();
            timer = 0;
        }
    }

    private void SpawnCoinRow()
    {
        int lane = Random.Range(0, lanePositions.Length);
        int count = Random.Range(3, 7);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(lanePositions[lane], coinHeight, spawnDistance + i * 2.5f);
            Instantiate(coinPrefab, pos, Quaternion.identity);
        }
    }
}
