using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : GenericStateMachine<PatrolManController>
    {
        public PatrolManStateMachine(PatrolManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<PatrolManController>(this));
            States.Add(StateMachine.States.PATROLLING, new PatrollingState<PatrolManController>(this));
            States.Add(StateMachine.States.CHASING, new ChasingState<PatrolManController>(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState<PatrolManController>(this));
        }
    }
}