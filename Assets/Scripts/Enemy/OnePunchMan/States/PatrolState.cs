using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrolState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private int currentPatrollingIndex = -1;
        
        public PatrolState(IStateMachine stateMachine) => this.stateMachine = stateMachine;
        public void OnStateEnter()
        {
            UpdatePatrollingIndex();
            Vector3 destination = GetNextDestination();
            MoveTowardsDestination(destination);
        }

        public void Update()
        {
            if (HasReachedDestination())
            {
                stateMachine.ChangeState(States.IDLE);
            }
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        private void UpdatePatrollingIndex()
        {
            if (currentPatrollingIndex == Owner.Data.PatrollingPoints.Count - 1)
            {
                currentPatrollingIndex = 0;
            }
            else
            {
                currentPatrollingIndex++;
            }
        }

        private Vector3 GetNextDestination() => Owner.Data.PatrollingPoints[currentPatrollingIndex];

        private void MoveTowardsDestination(Vector3 destination)
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(destination);
        }

        private bool HasReachedDestination() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
    }
}