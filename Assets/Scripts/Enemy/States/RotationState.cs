using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class RotationState : IState
    {
        public OnePunchManController Owner { get; set; }
        public OnePunchManStateMachine StateMachine { get; set; }
        private float targetRotation;
        
        public RotationState(OnePunchManStateMachine StateMachine)
        {
            this.StateMachine = StateMachine;
        }

        public void OnStateEnter() => targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;

        public void Update()
        {
            Owner.SetRotation(CalculateRotation());
            if(IsRotationComplete())
            {
                StateMachine.ChangeState(OnePunchManStates.IDLE);
            }
        }

        public void OnStateExit() => targetRotation = 0f;
        
        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
    }
}