using System;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public event Action<Vector2> OnTouchBegan;
    public event Action<Vector2> OnTouchMoved;
    public event Action<Vector2> OnTouchEnded;
    public event Action<Vector2> OnClick;

    private bool isDragging = false;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegan(touch.position);
                    break;

                case TouchPhase.Moved:
                    HandleTouchMoved(touch.position);
                    break;

                case TouchPhase.Ended:
                    HandleTouchEnded(touch.position);
                    break;
            }
        }
    }

    private void HandleTouchBegan(Vector2 touchPosition)
    {
        OnTouchBegan?.Invoke(touchPosition);
        isDragging = false;
    }

    private void HandleTouchMoved(Vector2 touchPosition)
    {
        OnTouchMoved?.Invoke(touchPosition);
        isDragging = true;
    }

    private void HandleTouchEnded(Vector2 touchPosition)
    {
        OnTouchEnded?.Invoke(touchPosition);

        if (isDragging)
            return;

        OnClick?.Invoke(touchPosition);
    }
}
