using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public class SimpleTouch
    {
        public enum TouchPhase
        {
            Unknown = -1,
            Began = 0,
            Moved = 1,
            Stationary = 2,
            Ended = 3,
            Canceled = 4,
            Hovered = 5
        }
        public TouchPhase phase = TouchPhase.Ended;
        public Vector2 position = Vector2.zero;
        public Vector2 deltaPosition = Vector2.zero;
    }

    public enum TouchMode
    {
        Viewport, World
    }
    public TouchMode touchMode = TouchMode.Viewport;

    #region Events
    public delegate void TouchEvent(SimpleTouch touch);
    public TouchEvent OnTouchBegin;
    public TouchEvent OnTouchContinue;
    public TouchEvent OnTouchEnd;
    public TouchEvent OnTouch;
    public TouchEvent OnHover;
    #endregion

    [Header("Status")]
    public SimpleTouch.TouchPhase phase;
    public Vector2 hovered;
    bool touching = false;
    SimpleTouch touch;

    private void Awake()
    {
        Input.simulateMouseWithTouches = true;
        touch = new SimpleTouch();
    }

    void Update()
    {
        SimpleTouch newTouch = new SimpleTouch();

        Vector2 newHovered;
        if (touchMode == TouchMode.Viewport)
        {
            newHovered = Camera.main.ScreenToViewportPoint(Input.mousePosition + Vector3.forward * 5);
        }
        else// if (touchMode == TouchMode.World)
        {
            newHovered = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 5);
        }


        if (Input.GetMouseButton(0))
        {
            touching = true;
            newTouch.position = newHovered;
            if (Input.GetMouseButtonDown(0))
            {
                newTouch.phase = SimpleTouch.TouchPhase.Began;
                newTouch.deltaPosition = Vector2.zero;
                OnTouchBegin?.Invoke(newTouch);
            }
            else
            {
                newTouch.deltaPosition = newTouch.position - touch.position;
                if (newTouch.deltaPosition != Vector2.zero)
                {
                    newTouch.phase = SimpleTouch.TouchPhase.Moved;
                }
                else
                {
                    newTouch.phase = SimpleTouch.TouchPhase.Stationary;
                }
                OnTouchContinue?.Invoke(newTouch);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touching = false;
            newTouch.phase = SimpleTouch.TouchPhase.Ended;
            newTouch.position = touch.position;
            newTouch.deltaPosition = touch.deltaPosition;
            OnTouchEnd?.Invoke(newTouch);


        }
        else
        {
            touching = false;
            newTouch.phase = SimpleTouch.TouchPhase.Hovered;
            newTouch.position = newHovered;
            newTouch.deltaPosition = newTouch.position - touch.position;
            OnHover?.Invoke(newTouch);
        }

        OnTouch?.Invoke(newTouch);

        touch = newTouch;
    }

    private void OnDrawGizmos()
    {
        if (Camera.main != null && touch != null)
        {

            hovered = touch.position;
            if(touchMode == TouchMode.Viewport)
            {
                hovered = Camera.main.ViewportToWorldPoint((Vector3)hovered + Vector3.forward * 5);
            }
            phase = touch.phase;
            Gizmos.DrawWireCube(hovered, Vector3.one);
            Gizmos.DrawLine(hovered, hovered - touch.deltaPosition);
        }
    }
}
