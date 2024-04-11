using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrollingState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private int currentPatrollingIndex = -1;
        private Vector3 destination;

        public PatrollingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            SetNextWaypointIndex();
            destination = GetDestination();
            MoveTowardsDestination();
        }

        public void Update()
        {
            if (ReachedDestination())
            {
                if (typeof(T) == typeof(TitanisController))
                {
                    int random = Random.Range(0,100);
                
                    if (random <= 70)
                        stateMachine.ChangeState(States.IDLE);
                    else
                    {
                        Owner.SetNextWaypoint(currentPatrollingIndex+1);
                        stateMachine.ChangeState(States.RAMPAGE);
                    }
                }
                else
                    stateMachine.ChangeState(States.IDLE);
            }
        }

        public void OnStateExit() { }

        private void SetNextWaypointIndex()
        {
            if (currentPatrollingIndex == Owner.Data.PatrollingPoints.Count-1)
                currentPatrollingIndex = 0;
            else
                currentPatrollingIndex++;
        }

        private Vector3 GetDestination() => Owner.Data.PatrollingPoints[currentPatrollingIndex];

        private void MoveTowardsDestination()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(destination);
        }

        private bool ReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;

    }
}