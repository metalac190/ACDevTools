using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherState : State
{
    public override IEnumerator Enter()
    {
        Debug.Log("...Entering INTRO state");
        yield return null;
    }

    public override IEnumerator Exit()
    {
        Debug.Log("Exiting INTRO state...");
        yield return null;
    }
}
