using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class RotatingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private float targetRotation;

        public RotatingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() => targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;

        public void Update()
        {
            Owner.SetRotation(CalculateRotation());
            if (IsRotationComplete())
                stateMachine.ChangeState(States.IDLE);
        }

        public void OnStateExit() => targetRotation = 0;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
    }
}