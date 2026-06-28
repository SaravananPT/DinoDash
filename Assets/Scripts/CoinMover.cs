using UnityEngine;

public class CoinMover : MonoBehaviour
{
    public float rotateSpeed = 180f;

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        transform.Translate(Vector3.back * GameManager.Instance.gameSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (transform.position.z < -20f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.PlayCoin();
            ScoreManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
