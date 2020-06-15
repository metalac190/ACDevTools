using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayHelper
{

    /// <summary>
    /// returns true if Index is within list range.
    /// NOTE: does NOT do null checking at index location.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="listSize"></param>
    /// <returns></returns>
    public static bool IsValidIndex(int index, int listSize)
    {
        // if index is within range
        if(index >= 0 && index < listSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// If index is at the end of the list, loops back to beginning
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="listSize"></param>
    /// <returns></returns>
    public static int GetNextLoopedIndex(int currentIndex, int listSize)
    {
        // if we're at the end of our index range, start over
        if(currentIndex >= listSize - 1)
        {
            currentIndex = 0;
        }
        // otherwise increase it
        else
        {
            currentIndex++;
        }

        return currentIndex;
    }

    /// <summary>
    /// If index is at the beginning, goes back to the end of the list
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="listSize"></param>
    /// <returns></returns>
    public static int GetPreviousLoopedIndex(int currentIndex, int listSize)
    {
        // if we're at the end of our index range, start over
        if (currentIndex <= 0)
        {
            currentIndex = listSize-1;
        }
        // otherwise increase it
        else
        {
            currentIndex--;
        }

        return currentIndex;
    }
}
