using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ACDev.Samples
{
    public class UISampleController : MonoBehaviour
    {
        [SerializeField] FlashImage _flashImage = null;
        [SerializeField] Text _countText = null;
        [SerializeField] Slider _timeSlider = null;

        [SerializeField] GameObject _popupPanel = null;

        Coroutine _countRoutine = null;
        Coroutine _sliderRoutine = null;
        Coroutine _counterRoutine = null;

        void Update()
        {
            // image flashes
            // Start Flash Loop
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _flashImage.Flash(.2f, 0, 1, Color.red);
            }
            // Timers
            // Countdown Text
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(_countRoutine != null)
                {
                    StopCoroutine(_countRoutine);
                }
                _countRoutine = StartCoroutine(UITimer.CountDownTime(_countText, .1f, 5));
            }
            // Count Elapsed Text
            if (Input.GetKeyDown(KeyCode.S))
            {
                if(_countRoutine != null)
                {
                    StopCoroutine(_countRoutine);
                }
                _countRoutine = StartCoroutine(UITimer.CountElapsedTime(_countText, .1f, 5));
            }
            // Countdown Bar
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(_sliderRoutine != null)
                {
                    StopCoroutine(_sliderRoutine);
                }
                _sliderRoutine = StartCoroutine(UITimer.CountDownSlider(_timeSlider, 3));
            }
            // Duration Bar
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_sliderRoutine != null)
                {
                    StopCoroutine(_sliderRoutine);
                }
                _sliderRoutine = StartCoroutine(UITimer.CountUpSlider(_timeSlider, 3));
            }

            // panels
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _popupPanel.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                _popupPanel.SetActive(false);
            }

            // time
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("Start");
               _counterRoutine =  Timer.DelayActionRetriggerable(this, Test, 1.5f, _counterRoutine);
            }
        }

        void Test()
        {
            Debug.Log("Finished");
        }
    }
}

