using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    public event Action<Vector2> OnTouchBegan;
    public event Action<Vector2> OnTouchMoved;
    public event Action<Vector2> OnTouchEnded;
    public event Action<Vector2> OnClick;

    private bool _isDragging = false;
    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = EventSystem.current;
    }
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
        _isDragging = false;
    }

    private void HandleTouchMoved(Vector2 touchPosition)
    {
        OnTouchMoved?.Invoke(touchPosition);
        _isDragging = true;
    }

    private void HandleTouchEnded(Vector2 touchPosition)
    {
        OnTouchEnded?.Invoke(touchPosition);

        if (_isDragging || IsPointerOverUIObject(touchPosition))
            return;

        OnClick?.Invoke(touchPosition);

    }

    private bool IsPointerOverUIObject(Vector2 touchPosition)
    {
        if (_eventSystem == null)
            return false;

        PointerEventData eventData = new PointerEventData(_eventSystem);
        eventData.position = touchPosition;

        var results = new List<RaycastResult>();
        _eventSystem.RaycastAll(eventData, results);

        return results.Count > 0;
    }
}
