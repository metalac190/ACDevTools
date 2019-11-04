using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Using this as a base class for the MainMenuStatemachine.
/// MainMenuStates inherit from this class, and then called Enter() and Exit() to
/// call events appropriately.
/// </summary>
public class ApplicationState : State
{
    // easy access to the StateController, so that we can call Change<state>
    protected ApplicationSM StateController { get; private set; }

    protected virtual void Awake()
    {
        StateController = GetComponent<ApplicationSM>();
    }
}
