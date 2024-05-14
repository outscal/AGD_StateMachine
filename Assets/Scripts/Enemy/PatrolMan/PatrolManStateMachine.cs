using System.Collections.Generic;
using StateMachine;
using StatePattern.StateMachine;
using StatePattern.Utilities;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : GenericStateMachine<PatrolManController>
    {
        private PatrolManController Owner;
        private IState currentState;
        protected Dictionary<States, IState> states = new Dictionary<States, IState>();

        public PatrolManStateMachine(PatrolManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(States.IDLE, new IdleState<PatrolManController>(this));
            states.Add(States.PATROLLING, new PatrollingState<PatrolManController>(this));
            states.Add(States.CHASING, new ChasingState<PatrolManController>(this));
            states.Add(States.SHOOTING, new ShootingState<PatrolManController>(this));
        }
    }
}