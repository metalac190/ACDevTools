using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleStatemachine : StateMachine
{
    public string PlayerName { get; private set; } = "Adam the Magnificent";

    public ExampleState ExampleState { get; private set; }
    public OtherState OtherState { get; private set; }

    private void Start()
    {
        ExampleState = new ExampleState(this);
        OtherState = new OtherState();

        ChangeState(OtherState);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(ExampleState);
        }
    }
}
