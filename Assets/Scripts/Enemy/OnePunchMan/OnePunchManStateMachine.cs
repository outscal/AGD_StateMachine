using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController>
    {
        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<OnePunchManController>(this));
            States.Add(StateMachine.States.ROTATING, new RotatingState<OnePunchManController>(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState<OnePunchManController>(this));
        }
    }
}