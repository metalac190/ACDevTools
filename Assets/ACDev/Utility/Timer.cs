using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Timer
{
    public static Coroutine DelayAction
        (this MonoBehaviour monoBehaviour, Action action, float time)
    {
        return monoBehaviour.StartCoroutine(DelayActionRoutine(action, time));
    }

    private static IEnumerator DelayActionRoutine(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}

