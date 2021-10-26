using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineMonoBehaviour : MonoBehaviour
{
	public StateInterface CurrentState { get; private set; }
	public StateInterface _previousState;

	bool _inTransition = false;

	public void ChangeState(StateInterface newState)
	{
		// condition to check that ready for new state or not
		if (CurrentState == newState || _inTransition)
			return;

		ChangeStateRoutine(newState);
	}

	public void RevertState()
	{
		if (_previousState != null)
			ChangeState(_previousState);
	}

	void ChangeStateRoutine(StateInterface newState)
	{
		_inTransition = true;
		// exit sequence and prepare for new state
		if (CurrentState != null)
			CurrentState.Exit();
		// save current state, if we want to return to it
		if (_previousState != null)
			_previousState = CurrentState;

		CurrentState = newState;

		// Enter sequence
		if (CurrentState != null)
			CurrentState.Enter();

		_inTransition = false;
	}

	
	public void Update()
	{
		// simulate update ticks in states because they dont have monobehaviour
		if (CurrentState != null && !_inTransition)
			CurrentState.Tick();
	}

	public void FixedUpdate()
	{
		// simulate fixedUpdate ticks in states
		if (CurrentState != null && !_inTransition)
			CurrentState.FixedTick();
	}
}
