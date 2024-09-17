using StatePattern.StateMachine;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : IStateMachine
    {
        private OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();

        public OnePunchManStateMachine(OnePunchManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState(this));
            States.Add(StateMachine.States.ROTATING, new RotatingState(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach(IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState) => ChangeState(States[newState]);
    }
}