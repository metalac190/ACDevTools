using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleState : State
{
    ExampleStatemachine _exampleStatemachine;

    public ExampleState(ExampleStatemachine stateMachine) : base()
    {
        _exampleStatemachine = stateMachine;
    }

    public override IEnumerator Enter()
    {
        Debug.Log("State has started!");

        yield return new WaitForSeconds(1.5f);

        Debug.Log("Player Name: " + _exampleStatemachine.PlayerName);
        yield return null;
    }
}
