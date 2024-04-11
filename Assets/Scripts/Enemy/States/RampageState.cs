using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class RampageState<T> :  IState where T :  EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private int index;
        private Vector3 destination;
        private const float RampageSpeed = 50f;
        
        public RampageState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            destination = Owner.Data.PatrollingPoints[Owner.NextWaypointIndex];
            MoveTowardsDestination();
        }
        
        public void Update()
        {
            if(RemainingDistance())
                stateMachine.ChangeState(States.IDLE);
        }

        public void OnStateExit() => Owner.Agent.speed = Owner.Data.MovementSpeed;

        private void MoveTowardsDestination()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.speed = RampageSpeed;
            Owner.Agent.SetDestination(destination);
        }

        private bool RemainingDistance() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;


    }
}