using System.Collections.Generic;

namespace StatePattern.Enemy.PatrolMan
{
    public class PatrolManStateMachine : IStateMachine
    {
        private PatrolManController Owner;
        private IState currentState;
        protected Dictionary<States, IState> states = new();

        public PatrolManStateMachine(PatrolManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        public void ChangeState(States newState) => ChangeState(states[newState]);

        public void Update() => currentState?.Update();

        private void CreateStates()
        {
            states.Add(States.IDLE,new IdleState(this));
            states.Add(States.PATROLLING,new PatrolState(this));
            states.Add(States.CHASING,new ChasingState(this));
            states.Add(States.SHOOTING,new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach (IState state in states.Values)
            {
                state.Owner = Owner;
            }
        }

        private void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
    }
}