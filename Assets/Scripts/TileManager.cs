using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public int tilesOnScreen = 7;
    public float tileLength = 20f;

    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnZ = 0f;
    private float recycleZ = -20f;

    private void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        MoveTiles();

        if (activeTiles.Count > 0 && activeTiles[0].transform.position.z < recycleZ)
            RecycleTile();
    }

    private void MoveTiles()
    {
        foreach (var tile in activeTiles)
            tile.transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime);
    }

    private void SpawnTile()
    {
        int index = Random.Range(0, tilePrefabs.Length);
        GameObject tile = Instantiate(tilePrefabs[index]);
        tile.transform.position = new Vector3(0, 0, spawnZ);
        activeTiles.Add(tile);
        spawnZ += tileLength;
    }

    private void RecycleTile()
    {
        GameObject tile = activeTiles[0];
        activeTiles.RemoveAt(0);
        tile.transform.position = new Vector3(0, 0, spawnZ);
        activeTiles.Add(tile);
        spawnZ += tileLength;
    }
}
