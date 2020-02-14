using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Timer
{
    public static Coroutine DelayActionRetriggerable(MonoBehaviour monoBehaviour, 
        Action action, float time, Coroutine coroutine)
    {
        if (coroutine != null)
            monoBehaviour.StopCoroutine(coroutine);
        return monoBehaviour.StartCoroutine(DelayActionRoutine(action, time));
    }
    public static Coroutine DelayAction
        (MonoBehaviour monoBehaviour, Action action, float time)
    {
        return monoBehaviour.StartCoroutine(DelayActionRoutine(action, time));
    }

    private static IEnumerator DelayActionRoutine(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}

