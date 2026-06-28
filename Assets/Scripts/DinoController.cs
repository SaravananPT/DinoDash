using UnityEngine;
using System.Collections;

public class DinoController : MonoBehaviour
{
    [Header("Lane Settings")]
    public float[] lanePositions = { -2.5f, 0f, 2.5f };
    public float laneSwitchSpeed = 12f;
    private int currentLane = 1;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float gravity = -25f;
    private float verticalVelocity;
    private bool isGrounded = true;
    private bool isJumping = false;

    [Header("Slide Settings")]
    public float slideDuration = 0.8f;
    public float slideColliderHeight = 0.5f;
    public float normalColliderHeight = 1.8f;
    private bool isSliding = false;

    private Animator anim;
    private CharacterController cc;
    private bool isDead = false;
    private Vector3 targetPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        SwipeDetector.Instance.OnSwipeLeft += MoveLeft;
        SwipeDetector.Instance.OnSwipeRight += MoveRight;
        SwipeDetector.Instance.OnSwipeUp += Jump;
        SwipeDetector.Instance.OnSwipeDown += Slide;
    }

    private void OnDisable()
    {
        SwipeDetector.Instance.OnSwipeLeft -= MoveLeft;
        SwipeDetector.Instance.OnSwipeRight -= MoveRight;
        SwipeDetector.Instance.OnSwipeUp -= Jump;
        SwipeDetector.Instance.OnSwipeDown -= Slide;
    }

    private void Update()
    {
        if (isDead) return;

        // Smooth lane movement
        float targetX = lanePositions[currentLane];
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, laneSwitchSpeed * Time.deltaTime);
        transform.position = pos;

        // Apply gravity
        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        verticalVelocity += gravity * Time.deltaTime;
        Vector3 move = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        CollisionFlags flags = cc.Move(move);

        isGrounded = (flags & CollisionFlags.Below) != 0;

        if (isGrounded && isJumping)
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }

        anim.SetBool("isGrounded", isGrounded);
    }

    private void MoveLeft()
    {
        if (isDead || isSliding) return;
        if (currentLane > 0)
        {
            currentLane--;
            SoundManager.Instance.PlaySwipe();
            anim.SetTrigger("moveLeft");
        }
    }

    private void MoveRight()
    {
        if (isDead || isSliding) return;
        if (currentLane < lanePositions.Length - 1)
        {
            currentLane++;
            SoundManager.Instance.PlaySwipe();
            anim.SetTrigger("moveRight");
        }
    }

    private void Jump()
    {
        if (isDead || !isGrounded || isSliding) return;
        verticalVelocity = jumpForce;
        isJumping = true;
        isGrounded = false;
        SoundManager.Instance.PlayJump();
        anim.SetBool("isJumping", true);
        anim.SetTrigger("jump");
    }

    private void Slide()
    {
        if (isDead || isSliding || !isGrounded) return;
        StartCoroutine(DoSlide());
    }

    private IEnumerator DoSlide()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        cc.height = slideColliderHeight;
        cc.center = new Vector3(0, slideColliderHeight / 2f, 0);
        SoundManager.Instance.PlaySlide();

        yield return new WaitForSeconds(slideDuration);

        isSliding = false;
        anim.SetBool("isSliding", false);
        cc.height = normalColliderHeight;
        cc.center = new Vector3(0, normalColliderHeight / 2f, 0);
    }

    private void OnTriggerEnter(Collider col)
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
