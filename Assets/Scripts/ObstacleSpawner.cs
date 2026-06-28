using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float[] lanePositions = { -2.5f, 0f, 2.5f };
    public float spawnDistance = 60f;
    public float minSpawnInterval = 1.2f;
    public float maxSpawnInterval = 2.8f;

    private float timer;
    private float nextSpawn;

    private void Start() => nextSpawn = Random.Range(minSpawnInterval, maxSpawnInterval);

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        timer += Time.deltaTime;
        if (timer >= nextSpawn)
        {
            SpawnObstacle();
            timer = 0;
            nextSpawn = Mathf.Max(0.8f, Random.Range(minSpawnInterval, maxSpawnInterval)
                - GameManager.Instance.gameSpeed * 0.01f);
        }
    }

    private void SpawnObstacle()
    {
        // Pick 1 or 2 blocked lanes — always leave at least 1 free
        int blockedCount = Random.value < 0.3f ? 2 : 1;
        int[] blocked = GetRandomLanes(blockedCount);

        foreach (int lane in blocked)
        {
            int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
            Vector3 pos = new Vector3(lanePositions[lane], 0, spawnDistance);
            Instantiate(obstaclePrefabs[prefabIndex], pos, Quaternion.identity);
        }
    }

    private int[] GetRandomLanes(int count)
    {
        int[] all = { 0, 1, 2 };
        for (int i = 0; i < all.Length; i++)
        {
            int j = Random.Range(i, all.Length);
            (all[i], all[j]) = (all[j], all[i]);
        }
        int[] result = new int[count];
        for (int i = 0; i < count; i++) result[i] = all[i];
        return result;
    }
}
