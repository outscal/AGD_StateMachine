using System.Collections.Generic;
using StateMachine;
using StatePattern.Enemy;
using StatePattern.StateMachine;

namespace StatePattern.Utilities
{
    public class GenericStateMachine<T> where T : EnemyController
    {
        protected T Owner;
        protected IState currentState;
        protected Dictionary<States, IState> states = new();

        public GenericStateMachine(T Owner) => this.Owner = Owner;

        public void Update() => currentState?.Update();

        public void ChangeState(States newState) => ChangeState(states[newState]);

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        protected void SetOwner()
        {
            foreach (IState state in states.Values)
            {
                state.Owner = Owner;
            }
        }
    }
}