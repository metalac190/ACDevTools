using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Apply this script to a panel to make it grow/shrink in a cycle.
/// Useful for hilighting a thing, or pulsing something to make it interesting.
/// Created by: Adam Chandler
/// NOTES: Panel must be 1,1,1 scale in order for this to work
/// </summary>

public class PanelPulse : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool _loopOnEnable = false;
    [SerializeField] private float _startingScale = 1;
    [SerializeField] float _pulseSpeedInSeconds = 1f;        // seconds for one pulse to complete
    public float PulseSpeedInSeconds
    {
        get { return _pulseSpeedInSeconds; }
        private set
        {
            if (value < 0)
            {
                value = 0;
            }
            _pulseSpeedInSeconds = value;
        }
    }
    [SerializeField] float _minSize = 1f;
    public float MinSize
    {
        get { return _minSize; }
        private set
        {
            if(value < 0)
            {
                value = 0;
            }
            _minSize = value;
        }
    }
    [SerializeField] float _maxSize = 1.1f;
    public float MaxSize
    {
        get { return _maxSize; }
        private set
        {
            if(value < 0)
            {
                value = 0;
            }
            _maxSize = value;
        }
    }

    // events
    public event Action OnStop = delegate { };
    public event Action OnCycleStart = delegate { };
    public event Action OnCycleComplete = delegate { };  // triggered everytime one pulse cycle is completed

    Coroutine _pulseRoutine = null;

    #region Initialization
    private void Awake()
    {
        SetDefaultPulseSize();
    }

    private void OnEnable()
    {
        if (_loopOnEnable)
        {
            Pulse();
        }
    }

    private void OnDisable()
    {
        if (_loopOnEnable)
        {
            StopPulse();
        }
    }
    #endregion

    #region Public Functions
    public void Pulse()
    {
        if (PulseSpeedInSeconds <= 0)
        {
            Debug.LogWarning("Cannot pulse at 0 speed");
            return;
        }     // 0 pulse speed doesn't make sense

        if (_pulseRoutine != null)
        {
            StopCoroutine(_pulseRoutine);
        }
        _pulseRoutine = StartCoroutine(PulseOnce(PulseSpeedInSeconds, MinSize, MaxSize));
    }

    public void Pulse(float secondsForOnePulse, float minSize, float maxSize)
    {
        if (PulseSpeedInSeconds <= 0)
        {
            Debug.LogWarning("Cannot pulse at 0 speed");
            return;
        }     // 0 pulse speed doesn't make sense

        SetNewPulseValues(secondsForOnePulse, minSize, maxSize);

        if (_pulseRoutine != null)
        {
            StopCoroutine(_pulseRoutine);
        }
        _pulseRoutine = StartCoroutine(PulseOnce(PulseSpeedInSeconds, MinSize, MaxSize));
    }

    public void StartPulseLoop()
    {
        if (PulseSpeedInSeconds <= 0)
        {
            Debug.LogWarning("Cannot pulse at 0 speed");
            return;
        }     // 0 pulse speed doesn't make sense

        if (_pulseRoutine != null)
        {
            StopCoroutine(_pulseRoutine);
        }
        _pulseRoutine = StartCoroutine(PulseLoop(PulseSpeedInSeconds, MinSize, MaxSize));
    }

    public void StartPulseLoop(float secondsForOnePulse, float minSize, float maxSize)
    {
        if (PulseSpeedInSeconds <= 0)
        {
            Debug.LogWarning("Cannot pulse at 0 speed");
            return;
        }     // 0 pulse speed doesn't make sense

        SetNewPulseValues(secondsForOnePulse, minSize, maxSize);

        if (_pulseRoutine != null)
        {
            StopCoroutine(_pulseRoutine);
        }
        _pulseRoutine = StartCoroutine(PulseLoop(PulseSpeedInSeconds, MinSize, MaxSize));
    }

    public void StopPulse()
    {
        if (_pulseRoutine != null)
        {
            StopCoroutine(_pulseRoutine);
        }

        OnStop.Invoke();
    }
    #endregion

    #region Private Functions
    IEnumerator PulseOnce(float secondsForOnePulse, float minSize, float maxSize)
    {
        // half the pulse time should be for grow, and half for shrink
        float growDuration = secondsForOnePulse / 2;
        float shrinkDuration = secondsForOnePulse / 2;

        float currentScaleAdjustment;

        OnCycleStart.Invoke();
        // growth cycle
        for (float t = 0; t <= growDuration; t += Time.deltaTime)
        {
            currentScaleAdjustment = Mathf.Lerp(minSize, maxSize, t / growDuration);
            transform.localScale = new Vector3
                (currentScaleAdjustment, currentScaleAdjustment, currentScaleAdjustment);
            yield return null;
        }
        // shrink cycle
        for (float t = 0; t <= shrinkDuration; t += Time.deltaTime)
        {
            currentScaleAdjustment = Mathf.Lerp(maxSize, minSize, t / shrinkDuration);
            transform.localScale = new Vector3
                (currentScaleAdjustment, currentScaleAdjustment, currentScaleAdjustment);
            yield return null;
        }

        OnCycleComplete.Invoke();
    }

    IEnumerator PulseLoop(float secondsForOnePulse, float minSize, float maxSize)
    {
        // half the pulse time should be for grow, and half for shrink
        float growDuration = secondsForOnePulse / 2;
        float shrinkDuration = secondsForOnePulse / 2;
        // start the pulse loop
        while (true)
        {
            float currentScaleAdjustment;

            OnCycleStart.Invoke();
            // growth cycle
            for (float t = 0; t <= growDuration; t += Time.deltaTime)
            {
                currentScaleAdjustment = Mathf.Lerp(minSize, maxSize, t / growDuration);
                transform.localScale = new Vector3
                    (currentScaleAdjustment, currentScaleAdjustment, currentScaleAdjustment);
                yield return null;
            }
            // shrink cycle
            for (float t = 0; t <= shrinkDuration; t += Time.deltaTime)
            {
                currentScaleAdjustment = Mathf.Lerp(maxSize, minSize, t / shrinkDuration);
                transform.localScale = new Vector3
                    (currentScaleAdjustment, currentScaleAdjustment, currentScaleAdjustment);
                yield return null;
            }

            OnCycleComplete.Invoke();
        }
    }

    private void SetDefaultPulseSize()
    {
        transform.localScale = new Vector3(_startingScale, _startingScale, _startingScale);
    }

    private void SetNewPulseValues(float secondsForOnePulse, float minSize, float maxSize)
    {
        PulseSpeedInSeconds = secondsForOnePulse;
        MinSize = minSize;
        MaxSize = maxSize;
    }
    #endregion
}

