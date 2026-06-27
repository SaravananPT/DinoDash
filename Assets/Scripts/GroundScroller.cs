using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public float groundWidth = 20f;
    public Transform[] groundTiles;

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        foreach (Transform tile in groundTiles)
        {
            tile.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

            if (tile.position.x < -groundWidth)
            {
                tile.position += new Vector3(groundWidth * groundTiles.Length, 0, 0);
            }
        }
    }
}
