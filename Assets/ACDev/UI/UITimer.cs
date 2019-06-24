using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ACDev.UI
{
    public static class UITimer
    {
        public static IEnumerator CountElapsedTime(TextMeshProUGUI countText, 
            float updateFrequenceInSeconds, float maxTime)
        {
            float elapsedTime = 0;

            while (elapsedTime < maxTime)
            {
                countText.text = elapsedTime.ToString("F1");
                yield return new WaitForSeconds(updateFrequenceInSeconds);
                elapsedTime += updateFrequenceInSeconds;
            }
            // if we've hit the max, ensure it ends there
            countText.text = maxTime.ToString("F1");
        }

        public static IEnumerator CountDownTime(TextMeshProUGUI countText, 
            float updateFrequenceInSeconds, float maxTime)
        {
            float timeRemaining = maxTime;

            while (timeRemaining > 0)
            {
                countText.text = timeRemaining.ToString("F1");
                yield return new WaitForSeconds(updateFrequenceInSeconds);
                timeRemaining -= updateFrequenceInSeconds;
            }
            // if we've his 0, ensure it ends there
            countText.text = 0.ToString("F1");
        }

        public static IEnumerator CountDownSlider(Slider timerSlider, float timerDuration)
        {
            timerSlider.maxValue = timerDuration;

            float timeRemaining = timerDuration;

            while (timeRemaining > 0)
            {
                timerSlider.value = timeRemaining;
                timeRemaining -= Time.deltaTime;
                yield return null;
            }
        }

        public static IEnumerator CountUpSlider(Slider timerSlider, float timerDuration)
        {
            timerSlider.maxValue = timerDuration;

            float elapsedTime = 0;

            while (elapsedTime < timerDuration)
            {
                timerSlider.value = elapsedTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}


