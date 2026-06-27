using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

        if (transform.position.x < -15f)
            Destroy(gameObject);
    }
}
