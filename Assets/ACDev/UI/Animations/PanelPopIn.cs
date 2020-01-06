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
    [Header("Bounce Settings")]  
    [SerializeField] float _startingXScale = 1.5f;
    [SerializeField] float _startingYScale = .5f;
    [SerializeField] float _scaleSpeed = 0.1f;      // in seconds
    [SerializeField] float _overshoot = -0.1f;
    [SerializeField] float _overshootReturnSpeed = .1f;
       
    [Header("Fade Settings")]
    [SerializeField] float _startingOpacity = .5f;
    [SerializeField] float _opacityChangeSpeed = 0.1f;      // in seconds

    public event Action OnPopStart = delegate { };

    Coroutine _popRoutine = null;
    Coroutine _fadeRoutine = null;

    CanvasGroup _canvasGroup = null;
    RectTransform _panelToAnimate = null;

    #region Initialization
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _panelToAnimate = GetComponent<RectTransform>();

        SetInitialValues();
    }

    private void OnEnable()
    {
        PopIn();
    }

    private void OnDisable()
    {
        StopBounce();
    }
    #endregion

    private void PopIn()
    {
        if(_popRoutine != null)
        {
            StopCoroutine(_popRoutine);
        }
        _popRoutine = StartCoroutine(PopInCycle());

        if(_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }
        _fadeRoutine = StartCoroutine(FadeInCycle());

        OnPopStart.Invoke();
    }

    private void StopBounce()
    {
        // cancel pop
        if (_popRoutine != null)
        {
            StopCoroutine(_popRoutine);
        }
        // cancel fade
        if(_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }
    }

    IEnumerator PopInCycle()
    {
        float newXScale;
        float newYScale;

        float destinationXScale = 1 + _overshoot;
        float destinationYScale = 1 + _overshoot;
        // growth cycle
        for (float t = 0; t <= _scaleSpeed; t += Time.deltaTime)
        {
            // adjust current size
            newXScale = Mathf.Lerp(_startingXScale, destinationXScale, t / _scaleSpeed);
            newYScale = Mathf.Lerp(_startingYScale, destinationYScale, t / _scaleSpeed);
            _panelToAnimate.localScale = new Vector3(newXScale, newYScale, 1);
            yield return null;
        }

        // our starting point was the previous destination
        float overshootStartXScale = destinationXScale;
        float overshootStartYScale = destinationYScale;
        // and our new destination is our standard scale/size
        destinationXScale = 1;
        destinationYScale = 1;
        // bounce back, with overshoot
        for (float t = 0; t <= _overshootReturnSpeed; t += Time.deltaTime)
        {
            // adjust current size
            newXScale = Mathf.Lerp(overshootStartXScale, destinationXScale, t / _overshootReturnSpeed);
            newYScale = Mathf.Lerp(overshootStartYScale, destinationYScale, t / _overshootReturnSpeed);
            _panelToAnimate.localScale = new Vector3(newXScale, newYScale, 1);
            yield return null;
        }
        // ensure that we've hit our normal scale, just in case
        _panelToAnimate.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator FadeInCycle()
    {
        float newOpacityValue;
        // fade in
        for (float t = 0; t <= _opacityChangeSpeed; t += Time.deltaTime)
        {
            newOpacityValue = Mathf.Lerp(_startingOpacity, 1, t / _opacityChangeSpeed);
            _canvasGroup.alpha = newOpacityValue;

            yield return null;
        }
        // ensure that we've ended fully opaque, just in case
        _canvasGroup.alpha = 1;
    }

    void SetInitialValues()
    {
        _panelToAnimate.localScale = new Vector3(_startingXScale, _startingYScale, 1);
        _canvasGroup.alpha = _startingOpacity;
    }
}


