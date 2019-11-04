using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this class to create a new state.
/// Override Enter() and Exit() to make things happen at the appropriate time,
/// as they will get called from the state machine.
/// NOTE: Do no try to change state while in mid transition.
/// </summary>
public abstract class State : MonoBehaviour
{
    // override this function to make things happen on State Enter
    public virtual void Enter()
    {

    }
    // override this function to make things happen on State Exit
    public virtual void Exit()
    {

    }
}
