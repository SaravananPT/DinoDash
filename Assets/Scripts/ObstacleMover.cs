using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);
    }
}
