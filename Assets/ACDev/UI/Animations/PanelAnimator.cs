using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Procedural animation through static classes for UI transforms
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.UI
{
    public static class PanelAnimator
    {
        public static IEnumerator ScaleAnimation(Transform panelToAnimate, float scaleSpeedInSeconds, float startScale, float endScale)
        {
            // scale value that recalculates every frame
            float newXYScale;
            // growth cycle
            for (float t = 0; t < 1.0f; t += Time.deltaTime / scaleSpeedInSeconds)
            {
                newXYScale = Mathf.Lerp(startScale, endScale, t);
                panelToAnimate.localScale = new Vector2(newXYScale, newXYScale);
                yield return null;
            }
            // ensure we hit our end point
            panelToAnimate.localScale = new Vector2(endScale, endScale);
        }

        public static IEnumerator MovePanel(Transform panelToAnimate, float moveDuration, Vector2 startPos, Vector2 endPos)
        {
            float currentXPos = startPos.x;
            float currentYPos = startPos.y;
            // animate
            for (float t = 0; t < 1.0f; t += Time.deltaTime / moveDuration)
            {
                currentXPos = Mathf.Lerp(startPos.x, endPos.x, t);
                currentYPos = Mathf.Lerp(startPos.y, endPos.y, t);
                panelToAnimate.localPosition = new Vector2(currentXPos, currentYPos);
                yield return null;
            }
            // ensure we've hit our end point
            panelToAnimate.localPosition = new Vector2(endPos.x, endPos.y);
        }

        public static IEnumerator ScaleText(Text textUI, float scaleSpeed, int endFontSize)
        {
            int startFontSize = textUI.fontSize;
            int currentFontSize = startFontSize;
            // keeping track of decimal separately, so that we can round to int
            float currentFontSizeExact = currentFontSize;
            // growth cycle
            for (float t = 0; t < 1.0f; t += Time.deltaTime / scaleSpeed)
            {

                currentFontSizeExact = Mathf.Lerp(startFontSize, endFontSize, t);
                currentFontSize = Mathf.RoundToInt(currentFontSizeExact);
                textUI.fontSize = currentFontSize;
                yield return null;
            }
            // ensure we hit our end point
            textUI.fontSize = endFontSize;
        }
    }
}


