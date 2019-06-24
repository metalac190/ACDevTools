using System.Collections;
using System.Collections.Generic;
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
namespace ACDev.UI
{
    [RequireComponent(typeof(Image))]
    public class FlashImage : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] bool _playOnAwake = false;
        [SerializeField] float _secondsForOneFlash = 2f;
        public float SecondsForOneFlash
        {
            get { return _secondsForOneFlash; }
            private set
            {
                if(value < 0)
                {
                    value = 0;
                }
                _secondsForOneFlash = value;
            }
        }
        [Range(0, 1)] [SerializeField] float _startingAlpha = 0;
        [Range(0, 1)] [SerializeField] float _minAlpha = 0f;
        [Range(0, 1)] [SerializeField] float _maxAlpha = 1f;

        public event Action OnFlashStart = delegate { };
        public event Action OnFlashStop = delegate { };
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

        private void OnEnable()
        {
            if (_playOnAwake == true)
            {
                StartFlashing();
            }
        }
        #endregion

        #region Public Functions
        public void StartFlashing()
        {
            if(_secondsForOneFlash <= 0) { return; }    // 0 speed wouldn't make sense

            if(_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }
            _flashRoutine = StartCoroutine(FlashLoop(SecondsForOneFlash, _minAlpha, _maxAlpha));

            OnFlashStart.Invoke();
        }
        public void StartFlashing(float secondsForOneFlash, float minAlpha, float maxAlpha)
        {
            if (_secondsForOneFlash <= 0) { return; }    // 0 speed wouldn't make sense

            minAlpha = Mathf.Clamp(minAlpha, 0, 1);
            maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }
            _flashRoutine = StartCoroutine(FlashLoop(secondsForOneFlash, minAlpha, maxAlpha));

            OnFlashStart.Invoke();
        }
        public void StopFlashing()
        {
            if(_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }
            SetAlphaToDefault();

            OnFlashStop.Invoke();
        }
        #endregion

        IEnumerator FlashLoop(float secondsForOneFlash, float minAlpha, float maxAlpha)
        {
            // half the flash time should be on flash in, the other half for flash out
            float flashInDuration = secondsForOneFlash / 2;
            float flashOutDuration = secondsForOneFlash / 2;
            // start the flash cycle
            while (true)
            {
                // flash in
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

                OnCycleComplete.Invoke();
            }
        }
        private void SetAlphaToDefault()
        {
            Color newColor = _flashImage.color;
            newColor.a = _startingAlpha;
            _flashImage.color = newColor;
        }

    }
}

