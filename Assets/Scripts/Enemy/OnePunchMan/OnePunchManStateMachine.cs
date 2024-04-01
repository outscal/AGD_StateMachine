using System.Collections.Generic;
using StatePattern.Enemy.States;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine
    {
        private OnePunchManController owner;
        private IState currentState;
        private Dictionary<OnePunchManStates, IState> states = new();

        public OnePunchManStateMachine(OnePunchManController owner)
        {
            this.owner = owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(OnePunchManStates.IDLE,new IdleState(this));
            states.Add(OnePunchManStates.ROTATION,new RotationState(this));
            states.Add(OnePunchManStates.SHOOTING,new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach (IState state in states.Values)
            {
                state.Owner = owner;
            }
        }

        private void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void Update() => currentState?.Update();

        public void ChangeState(OnePunchManStates newState) => ChangeState(states[newState]);


    }
}