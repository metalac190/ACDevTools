using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the transitions between multiple states. Inherit from this
/// class and then use ChangeState<StateToChange>(); to transition between states
/// </summary>
public class StateMachine : MonoBehaviour
{
    protected State _currentState;
    public virtual State CurrentState
    {
        get { return _currentState; }
        set { Transition(value); }
    }
    protected bool _inTransition;

    // Gets a state component from the StateMachine
    public virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();
        if(target == null)
        {
            target = gameObject.AddComponent<T>();
        }
        return target;
    }
    
    // Transition to a new state
    public virtual void ChangeState<T>() where T : State
    {
        CurrentState = GetState<T>();
    }

    private void Transition(State newState)
    {
        // if it's the same state, ignore transition
        if(_currentState == newState || _inTransition) { return; }
        // we are now transitioning
        _inTransition = true;
        // handle the previous state first
        _currentState?.Exit();
        // assign new state
        _currentState = newState;
        // enter new state
        _currentState?.Enter();
        // no longer transitioning
        _inTransition = false;
    }
}
