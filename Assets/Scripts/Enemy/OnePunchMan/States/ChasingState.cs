namespace StatePattern.Enemy
{
    public class ChasingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;

        public ChasingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            Owner.Agent.isStopped = true;
            MoveTowardsTarget();
        }

        public void Update()
        {
            if (HasReachedDestination())
            {
                stateMachine.ChangeState(States.SHOOTING);
            }
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }

        private void MoveTowardsTarget()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(Owner.Target);
        }

        private bool HasReachedDestination()
        {
            if (Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            {
                return true;
            }

            return false;
        }
    }
}