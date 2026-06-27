using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layer;
        public float speedMultiplier;
    }

    public ParallaxLayer[] layers;

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        foreach (var pl in layers)
        {
            pl.layer.Translate(Vector2.left * GameManager.Instance.gameSpeed * pl.speedMultiplier * Time.deltaTime);

            if (pl.layer.position.x < -30f)
                pl.layer.position += new Vector3(60f, 0, 0);
        }
    }
}
