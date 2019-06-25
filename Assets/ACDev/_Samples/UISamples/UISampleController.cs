using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ACDev.UI;
using TMPro;
using UnityEngine.UI;

namespace ACDev.Samples
{
    public class UISampleController : MonoBehaviour
    {
        [SerializeField] FlashImage _flashImage = null;
        [SerializeField] TextMeshProUGUI _countText = null;
        [SerializeField] Slider _timeSlider = null;

        Coroutine _countRoutine = null;
        Coroutine _sliderRoutine = null;

        void Update()
        {
            // image flashes
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _flashImage.StartFlashing();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _flashImage.StopFlashing();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _flashImage.StartFlashing(1, 0, .5f);
            }

            // Timers
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(_countRoutine != null)
                {
                    StopCoroutine(_countRoutine);
                }
                _countRoutine = StartCoroutine(UITimer.CountDownTime(_countText, .1f, 5));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_countRoutine != null)
                {
                    StopCoroutine(_countRoutine);
                }
                _countRoutine = StartCoroutine(UITimer.CountElapsedTime(_countText, .1f, 5));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(_sliderRoutine != null)
                {
                    StopCoroutine(_sliderRoutine);
                }
                _sliderRoutine = StartCoroutine(UITimer.CountDownSlider(_timeSlider, 3));
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_sliderRoutine != null)
                {
                    StopCoroutine(_sliderRoutine);
                }
                _sliderRoutine = StartCoroutine(UITimer.CountUpSlider(_timeSlider, 3));
            }
        }
    }
}

