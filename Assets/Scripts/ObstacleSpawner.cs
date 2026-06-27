using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;
    public Transform spawnPoint;

    private float timer;
    private float nextSpawnTime;

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        timer += Time.deltaTime;
        if (timer >= nextSpawnTime)
        {
            SpawnObstacle();
            timer = 0;
            SetNextSpawnTime();
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[index], spawnPoint.position, Quaternion.identity);
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
