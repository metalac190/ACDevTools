using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows you to easily convert Unity Colors into more usable data.
/// Created by: Adam Chandler
/// </summary>
namespace ACDev.Utility
{
    public static class ColorManagement
    {
        /// <summary>
        /// Converts RGBA values into 0-1, which is what default Unity color requires
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Color ConvertRGBAToColor(float r, float g, float b, float a)
        {
            Color newColor = new Color(r / 255, g / 255f, b / 255f, a / 255f);
            return newColor;
        }

        public static Vector3 ConvertColorToRGB(Color colorToConvert)
        {
            // convert each of the values to 0-255 range (from 0-1)
            // make sure values are whole numbers (so that we can convert to int)
            float r = Mathf.RoundToInt(colorToConvert.r * 255);
            float g = Mathf.RoundToInt(colorToConvert.g * 255);
            float b = Mathf.RoundToInt(colorToConvert.b * 255);
            // make sure values are between 0-255
            Mathf.Clamp(r, 0, 255);
            Mathf.Clamp(g, 0, 255);
            Mathf.Clamp(b, 0, 255);
            // assign these values into a new color and return it
            Vector3 newColor = new Vector3(r, g, b);
            return newColor;
        }

        public static Vector4 ConvertColorToRGBA(Color colorToConvert)
        {
            // convert each of the values to 0-255 range (from 0-1)
            // make sure values stay between 0-255
            float r = Mathf.Clamp(colorToConvert.r * 255, 0, 255);
            float g = Mathf.Clamp(colorToConvert.g * 255, 0, 255);
            float b = Mathf.Clamp(colorToConvert.b * 255, 0, 255);
            float a = Mathf.Clamp(colorToConvert.a * 255, 0, 255);
            // assign these values into a new color and return it
            Vector4 newColor = new Vector4(r, g, b, a);
            return newColor;
        }
    }
}

