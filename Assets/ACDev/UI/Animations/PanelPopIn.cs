using System.Collections;
using UnityEngine;
using System;

/// <summary>
/// This script adds a "PopIn" animation when this UI gameobject is enabled.
/// Created by: Adam Chandler
/// NOTES: This gameObject needs 1,1,1 scale in order to work properly. Attach to
/// any UI GameObject and it will animate in on Enable
/// </summary>

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class PanelPopIn : MonoBehaviour
{
    [SerializeField] private bool _playOnEnable = true;

    [Header("Bounce Settings")]
    [Range(0,2)]
    [SerializeField] private float _startingXScale = .8f;
    [Range(0,2)]
    [SerializeField] private float _startingYScale = .8f;
    [Range(0,1)]
    [SerializeField] private float _scaleSpeed = 0.2f;      // in seconds

    [Header("Bounce Overshoot")]
    [Range(0, 1)]
    [Tooltip("How far to scale past target to create a bounceback effect. This gets added" +
        "to 1.0 scale")]
    [SerializeField] private float _overshootScaleAmount = 0;
    [Range(0, 1)]
    [Tooltip("Seconds to go from overshoot back to the target scale")]
    [SerializeField] private float _overshootReturnSpeed = 0;
       
    [Header("Fade Settings")]
    [Range(0, 1)]
    [Tooltip("Starting opacity when fading begins")]
    [SerializeField] private float _startingOpacity = 0.5f;
    [Range(0, 1)]
    [Tooltip("Seconds to reach full opacity")]
    [SerializeField] private float _opacityChangeSpeed = 0.2f;      // in seconds

    [Header("Move Settings")]
    [Tooltip("Local offset from initial position where the move animation " +
        "will begin")]
    [SerializeField] private Vector2 _startPosOffset = new Vector2(0, 0);
    [Range(0, 1)]
    [SerializeField] private float _moveInSpeed = 0;

    public event Action AnimationStarted = delegate { };

    private Coroutine _popInRoutine = null;
    private Coroutine _fadeInRoutine = null;
    private Coroutine _moveInRoutine = null;

    private CanvasGroup _canvasGroup = null;
    private RectTransform _panelToAnimate = null;

    private Vector2 _startPos;

    #region Initialization
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _panelToAnimate = GetComponent<RectTransform>();

        SetInitialValues();
    }

    private void OnEnable()
    {
        if(_playOnEnable)
            Play();
    }

    private void OnDisable()
    {
        Stop();
    }
    #endregion

    public void Play()
    {
        if(_popInRoutine != null)
            StopCoroutine(_popInRoutine);
        _popInRoutine = StartCoroutine(PopInRoutine());

        if (_fadeInRoutine != null)
            StopCoroutine(_fadeInRoutine);
        _fadeInRoutine = StartCoroutine(FadeInRoutine());

        if (_moveInRoutine != null)
            StopCoroutine(_moveInRoutine);
        _moveInRoutine = StartCoroutine(MoveInRoutine());

        AnimationStarted.Invoke();
    }

    public void Stop()
    {
        if (_popInRoutine != null)
            StopCoroutine(_popInRoutine);

        if(_fadeInRoutine != null)
            StopCoroutine(_fadeInRoutine);

        if (_moveInRoutine != null)
            StopCoroutine(_moveInRoutine);
    }

    private IEnumerator PopInRoutine()
    {
        float newXScale;
        float newYScale;

        float destinationXScale = 1 + _overshootScaleAmount;
        float destinationYScale = 1 + _overshootScaleAmount;

        // growth cycle
        if(_scaleSpeed > 0)
        {
            for (float t = 0; t <= _scaleSpeed; t += Time.deltaTime)
            {
                // adjust current size
                newXScale = Mathf.Lerp(_startingXScale, destinationXScale, t / _scaleSpeed);
                newYScale = Mathf.Lerp(_startingYScale, destinationYScale, t / _scaleSpeed);
                _panelToAnimate.localScale = new Vector3(newXScale, newYScale, 1);
                yield return null;
            }
        }

        // our starting point was the previous destination
        float overshootStartXScale = destinationXScale;
        float overshootStartYScale = destinationYScale;
        // and our new destination is our standard scale/size
        destinationXScale = 1;
        destinationYScale = 1;
        // bounce back, with overshoot
        if(_overshootReturnSpeed > 0)
        {
            for (float t = 0; t <= _overshootReturnSpeed; t += Time.deltaTime)
            {
                // adjust current size
                newXScale = Mathf.Lerp(overshootStartXScale, destinationXScale, t / _overshootReturnSpeed);
                newYScale = Mathf.Lerp(overshootStartYScale, destinationYScale, t / _overshootReturnSpeed);
                _panelToAnimate.localScale = new Vector3(newXScale, newYScale, 1);
                yield return null;
            }
        }

        // ensure that we've hit our normal scale, just in case
        _panelToAnimate.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator FadeInRoutine()
    {
        float newOpacityValue;
        // fade in
        if(_opacityChangeSpeed > 0)
        {
            for (float t = 0; t <= _opacityChangeSpeed; t += Time.deltaTime)
            {
                newOpacityValue = Mathf.Lerp(_startingOpacity, 1, t / _opacityChangeSpeed);
                _canvasGroup.alpha = newOpacityValue;

                yield return null;
            }
        }

        // ensure that we've ended fully opaque, just in case
        _canvasGroup.alpha = 1;
    }

    IEnumerator MoveInRoutine()
    {
        float startPosX = _startPos.x + _startPosOffset.x;
        float startPosY = _startPos.y + _startPosOffset.y;
        float targetPosX = _startPos.x;
        float targetPosY = _startPos.y;
        // animate
        if (_moveInSpeed > 0)
        {
            float currentPosX = startPosX;
            float currentPosY = startPosY;

            for (float t = 0; t <= _moveInSpeed; t += Time.deltaTime)
            {
                currentPosX = Mathf.Lerp(startPosX, targetPosX, t / _moveInSpeed);
                currentPosY = Mathf.Lerp(startPosY, targetPosY, t / _moveInSpeed);
                _panelToAnimate.anchoredPosition = new Vector2(currentPosX, currentPosY);
                yield return null;
            }
        }

        // ensure we've hit our end point
        _panelToAnimate.anchoredPosition = new Vector2(targetPosX, targetPosY);
    }

    void SetInitialValues()
    {
        _panelToAnimate.localScale = new Vector3(_startingXScale, _startingYScale, 1);
        _canvasGroup.alpha = _startingOpacity;
        _startPos = _panelToAnimate.anchoredPosition;
    }
}


