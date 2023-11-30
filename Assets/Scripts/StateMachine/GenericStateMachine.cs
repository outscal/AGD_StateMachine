using StatePattern.Enemy;
using System.Collections.Generic;

namespace StatePattern.StateMachine
{
    public class GenericStateMachine<T> where T : EnemyController
    {
        protected T Owner;
        protected IState currentState;
        protected Dictionary<States, IState> States = new();

        public GenericStateMachine(T Owner) => this.Owner = Owner;

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState) => ChangeState(States[newState]);

        protected void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }
    }
}