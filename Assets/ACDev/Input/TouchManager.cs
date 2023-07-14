using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public event Action<Vector2> TouchPressed;
    public event Action<Vector2> TouchReleased;

    private bool _isTouching;
    private Vector2 _touchPos;

    public bool IsTouching => _isTouching;
    public Vector2 TouchPos => _touchPos;

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();

        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
        EnhancedTouch.Touch.onFingerUp += OnFingerUp;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();

        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
        EnhancedTouch.Touch.onFingerUp -= OnFingerUp;
    }

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        _isTouching = true;
        _touchPos = finger.currentTouch.screenPosition;

        TouchPressed?.Invoke(_touchPos);
    }

    private void OnFingerUp(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        _isTouching = false;
        _touchPos = new Vector2(0, 0);

        TouchReleased?.Invoke(_touchPos);
    }

}
