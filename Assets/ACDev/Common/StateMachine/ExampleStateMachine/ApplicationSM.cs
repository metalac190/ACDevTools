using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example of how to implement a State Machine. Keep track of important
/// game controllers on this object, and change state when appropriate
/// </summary>
public class ApplicationSM : StateMachine
{
    void Start()
    {
        ChangeState<ApplicationState>();
    }
}
