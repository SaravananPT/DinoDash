using UnityEngine;
using System;

public class SwipeDetector : MonoBehaviour
{
    public static SwipeDetector Instance;

    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;
    public event Action OnSwipeUp;
    public event Action OnSwipeDown;

    private Vector2 touchStartPos;
    private float minSwipeDistance = 50f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.Playing) return;

        HandleTouchInput();
        HandleKeyboardInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            touchStartPos = touch.position;

        if (touch.phase == TouchPhase.Ended)
        {
            Vector2 delta = touch.position - touchStartPos;

            if (delta.magnitude < minSwipeDistance) return;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0) OnSwipeRight?.Invoke();
                else OnSwipeLeft?.Invoke();
            }
            else
            {
                if (delta.y > 0) OnSwipeUp?.Invoke();
                else OnSwipeDown?.Invoke();
            }
        }
    }

    private void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) OnSwipeLeft?.Invoke();
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) OnSwipeRight?.Invoke();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) OnSwipeUp?.Invoke();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) OnSwipeDown?.Invoke();
    }
}
