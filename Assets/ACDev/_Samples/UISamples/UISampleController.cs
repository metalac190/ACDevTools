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
        [SerializeField] FlashImage _flashImage;
        [SerializeField] TextMeshProUGUI _countText;
        [SerializeField] Slider _timeSlider;

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
                StartCoroutine(UITimer.CountDownTime(_countText, .1f, 5));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(UITimer.CountElapsedTime(_countText, .1f, 5));
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(UITimer.CountDownSlider(_timeSlider, 3));
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(UITimer.CountUpSlider(_timeSlider, 3));
            }
        }
    }
}

