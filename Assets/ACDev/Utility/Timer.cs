using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ACDev.Utility
{
    public static class Timer
    {
        public static Coroutine Invoke(this MonoBehaviour monoBehaviour, Action action, float time)
        {
            return monoBehaviour.StartCoroutine(InvokeFunction(action, time));
        }

        private static IEnumerator InvokeFunction(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}

