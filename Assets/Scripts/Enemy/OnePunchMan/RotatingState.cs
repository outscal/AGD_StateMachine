using StatePattern.Enemy;
using StatePattern.StateMachine;
using UnityEngine;

public class RotatingState : IState
{
    public EnemyController Owner { get; set; }
    private OnePunchManStateMachine stateMachine;
    private float targetRotation;

    public RotatingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter() => targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;

    public void Update()
    {
        // Calculate and set the character's rotation based on the target rotation.
        Owner.SetRotation(CalculateRotation());
        if (IsRotationComplete())
            stateMachine.ChangeState(States.IDLE);
    }

    public void OnStateExit() => targetRotation = 0;

    private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);

    private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
}
