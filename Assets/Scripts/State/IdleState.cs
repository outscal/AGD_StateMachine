using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;
using StatePattern.State;

public class IdleState : IState
{
    public OnePunchManController Owner { get; set; }
    private OnePunchManStateMachine stateMachine;
    private float timer;

    public IdleState(OnePunchManStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter() => ResetTimer();

    public void OnStateExit() => timer = 0;

    public void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            stateMachine.ChangeState(OnePunchManState.ROTATING);
        }
    }


    public void ResetTimer() => timer = Owner.Data.IdleTime;
}
