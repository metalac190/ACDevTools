using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// This script will flash a UI Image component with the given parameters. Useful for creating
/// quick, animated UI flashes.
/// Created by: Adam Chandler
/// Make sure that you attach this script to an Image component. You can optionally call the
/// flash remotely and pass in new flash values, or you can predefine settings in the Inspector
/// </summary>

[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    // events
    public event Action OnStop = delegate { };
    public event Action OnCycleStart = delegate { };
    public event Action OnCycleComplete = delegate { };

    Coroutine _flashRoutine = null;
    Image _flashImage;

    #region Initialization
    private void Awake()
    {
        _flashImage = GetComponent<Image>();
        // initial state
        SetAlphaToDefault();
    }

    #endregion

    #region Public Functions

    public void Flash(float secondsForOneFlash, float minAlpha, float maxAlpha, Color color)
    {
        if (secondsForOneFlash <= 0) { return; }    // 0 speed wouldn't make sense

        if (_flashRoutine != null)
            StopCoroutine(_flashRoutine);
        _flashRoutine = StartCoroutine(
            FlashRoutine(secondsForOneFlash, minAlpha, maxAlpha, color)
            );
    }

    public void StopFlash()
    {
        if (_flashRoutine != null)
            StopCoroutine(_flashRoutine);

        SetAlphaToDefault();

        OnStop?.Invoke();
    }
    #endregion

    #region Private Functions
    IEnumerator FlashRoutine(float secondsForOneFlash, float minAlpha, float maxAlpha, Color color)
    {

        SetColor(color);
        // half the flash time should be on flash in, the other half for flash out
        float flashInDuration = secondsForOneFlash / 2;
        float flashOutDuration = secondsForOneFlash / 2;

        OnCycleStart?.Invoke();
        // flash in
        Debug.Log("Start Flash");
        for (float t = 0f; t <= flashInDuration; t += Time.deltaTime)
        {
            Color newColor = _flashImage.color;
            newColor.a = Mathf.Lerp(minAlpha, maxAlpha, t / flashInDuration);
            _flashImage.color = newColor;
            yield return null;
        }
        // flash out
        for (float t = 0f; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color newColor = _flashImage.color;
            newColor.a = Mathf.Lerp(maxAlpha, minAlpha, t / flashOutDuration);
            _flashImage.color = newColor;
            yield return null;
        }
        SetAlphaToDefault();
        OnCycleComplete?.Invoke();
    }

    private void SetColor(Color newColor)
    {
        _flashImage.color = newColor;
    }

    private void SetAlphaToDefault()
    {
        Color newColor = _flashImage.color;
        newColor.a = 0;
        _flashImage.color = newColor;
    }

    #endregion
}

