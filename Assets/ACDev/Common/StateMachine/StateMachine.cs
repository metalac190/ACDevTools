/// <summary>
/// Created by Adam Chandler, 2020
/// In order to implement this StateMachine, you need to do the following things:
/// 1. Create some sort of Controller that inherits from StateMachine
/// 2. Create a few classes that inherit from State, override functions as desired
/// 3. Create a few instances of the class somewhere in your new Controller
/// 4. Use ChangeState(state) to Change to any of the states in your Controller!
/// 5. If the States need more information, you can pass them down in the Constructor when the Controller initializes them
/// </summary>

using System.Collections;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
	public State Current { get; private set; }
	public State _previous;

	bool _inTransition = false;

	public virtual void Update()
	{
		if (!_inTransition)
		{
			Current?.Tick();
		}
	}

	public void ChangeState(State target)
	{
		if (Current == target || _inTransition)
			return;

		StartCoroutine(ChangeStateRoutine(target));
	}

	public void RevertState()
	{
		ChangeState(_previous);
	}

	IEnumerator ChangeStateRoutine(State target)
	{
		_inTransition = true;

		if (Current != null)
			yield return StartCoroutine(Current?.Exit());

		if (_previous != null)
			_previous = Current;

		Current = target;

		if (Current != null)
			yield return StartCoroutine(Current?.Enter());

		_inTransition = false;
	}
}
