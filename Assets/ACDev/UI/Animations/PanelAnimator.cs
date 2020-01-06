using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Procedural animation through static classes for UI transforms.
/// Created by: Adam Chandler
/// </summary>

public static class PanelAnimator
{
    public static IEnumerator ScaleAnimation(Transform panelToAnimate, float scaleSpeedInSeconds, float startScale, float endScale)
    {
        // scale value that recalculates every frame
        float newXYScale;
        // growth cycle
        for (float t = 0; t <= scaleSpeedInSeconds; t += Time.deltaTime)
        {
            newXYScale = Mathf.Lerp(startScale, endScale, t / scaleSpeedInSeconds);
            panelToAnimate.localScale = new Vector2(newXYScale, newXYScale);
            yield return null;
        }
        // ensure we hit our end point
        panelToAnimate.localScale = new Vector2(endScale, endScale);
    }

    public static IEnumerator MovePanel(Transform panelToAnimate, float moveSpeed, Vector2 startPos, Vector2 endPos)
    {
        float currentXPos = startPos.x;
        float currentYPos = startPos.y;
        // animate
        for (float t = 0; t <= moveSpeed; t += Time.deltaTime)
        {
            currentXPos = Mathf.Lerp(startPos.x, endPos.x, t / moveSpeed);
            currentYPos = Mathf.Lerp(startPos.y, endPos.y, t / moveSpeed);
            panelToAnimate.localPosition = new Vector2(currentXPos, currentYPos);
            yield return null;
        }
        // ensure we've hit our end point
        panelToAnimate.localPosition = new Vector2(endPos.x, endPos.y);
    }

    public static IEnumerator ScaleText(Text textUI, float scaleSpeed, int endFontSize)
    {
        int startFontSize = textUI.fontSize;

        // keeping track of decimal separately, so that we can round to int
        float fontSizeExact = startFontSize;
        int fontSizeRounded = startFontSize;
        // growth cycle
        for (float t = 0; t <= scaleSpeed; t += Time.deltaTime)
        {
            fontSizeExact = Mathf.Lerp(startFontSize, endFontSize, t / scaleSpeed);
            fontSizeRounded = Mathf.RoundToInt(fontSizeExact);
            textUI.fontSize = fontSizeRounded;
            yield return null;
        }
        // ensure we hit our end point
        textUI.fontSize = endFontSize;
    }
}


