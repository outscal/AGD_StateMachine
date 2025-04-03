using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.State;
using UnityEngine;

public class OnePunchManStateMachine
{
    private OnePunchManController Owner;
    private IState currentState;

    protected Dictionary<OnePunchManState, IState> states = new Dictionary<OnePunchManState, IState>();

    public OnePunchManStateMachine(OnePunchManController owner)
    {
        this.Owner = owner;
        CreateStates();
        SetOwner();
    }

    private void CreateStates()
    {
        states.Add(OnePunchManState.IDLE,new IdleState(this));
        states.Add(OnePunchManState.ROTATING, new RotatingState(this));
        states.Add(OnePunchManState.SHOOTING, new ShootingState(this));
    }

    private void SetOwner()
    {
        foreach(IState state in states.Values)
        {
            state.Owner = Owner;
        }
    }

    public void Update() => currentState.Update();

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(OnePunchManState newState) => ChangeState(states[newState]);
}
