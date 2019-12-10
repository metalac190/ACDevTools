using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script simulates Camera Shake by animating the local position of the Camera
/// Created by: Adam Chandler, based on Camera Shake tutorial by Brackys
/// NOTE: Apply this script to a Camera object that has it's local transforms 0'd out. The
/// easiest way to do this is to make sure the Camera is a child underneath a 'main' 
/// Camera object that inherits the Camera transforms.
/// </summary>
namespace ACDev.Animations
{
    public class CameraShake : MonoBehaviour
    {
        Coroutine _shakeRoutine = null;
        Vector3 _originalPos = new Vector3(0,0,0);

        public void Shake(float duration, float magnitude)
        {
            if(_shakeRoutine != null)
            {
                transform.localPosition = _originalPos;
                StopCoroutine(_shakeRoutine);
            }
            StartCoroutine(ShakeRoutine(duration, magnitude));
        }

        IEnumerator ShakeRoutine(float duration, float magnitude)
        {
            _originalPos = transform.localPosition;

            float elapsedTime = 0;

            while(elapsedTime < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, _originalPos.z);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = _originalPos;
        }
    }
}

