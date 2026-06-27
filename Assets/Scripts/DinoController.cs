using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
            Jump();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        anim.SetBool("isGrounded", isGrounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetTrigger("jump");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Obstacle") && !isDead)
        {
            isDead = true;
            anim.SetTrigger("die");
            GameManager.Instance.DinoHit();
        }
    }

    public void Revive()
    {
        isDead = false;
        anim.SetTrigger("revive");
    }
}
