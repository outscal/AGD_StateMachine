using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;
using StatePattern.State;

public class RotatingState : IState
{
    public OnePunchManController Owner { get; set; }
    private OnePunchManStateMachine stateMachine;
    private float targetRotation;

    public RotatingState(OnePunchManStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;
    }

    public void OnStateExit()
    {
        targetRotation = 0;
    }

    public void Update()
    {
        Owner.SetRotation(CalculateRotation());
        if(IsRotationComplete())
        {
            stateMachine.ChangeState(OnePunchManState.IDLE);
        }
    }

    private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);

    private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
}
