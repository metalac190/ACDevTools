
/// <summary>
/// State machine system modified from theliquidfire.com's example state machines.
/// To use:
/// 1. Create a partial class that contains an instance of your StateMachine
///     ( StateMachine _stateMachine = new StateMachine(); )
/// 2. Create a partial class with the SAME NAME as the state you'd like to create
///     Ex. IntroState.cs
/// 3. Create a State inside of our new ExampleState class that uses the State Constructor
///     Ex. _exampleState = new State(OnEnterExampleState, OnExitExampleState, "Example");
/// 4. Create the OnEnterExampleState()/Exit() functions below and fill in as needed
/// 5. Put controllers on the root class that contains the StateMachine
/// </summary>
[System.Serializable]
public class StateMachine {
	public State Current { get; private set; }

	public void ChangeState (State target)
	{
		if (Current == target)
			return;

		if (Current != null)
			Current.Exit();

		Current = target;

		if (Current != null)
			Current.Enter();
	}
}
