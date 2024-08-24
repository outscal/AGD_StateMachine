using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class HitmanStateMachine : GenericStateMachine<HitmanController>
    {
        public HitmanStateMachine(HitmanController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<HitmanController>(this));
            States.Add(StateMachine.States.PATROLLING, new PatrollingState<HitmanController>(this));
            States.Add(StateMachine.States.CHASING, new ChasingState<HitmanController>(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState<HitmanController>(this));
            States.Add(StateMachine.States.TELEPORTING, new TeleportingState<HitmanController>(this));
        }
    }
}

