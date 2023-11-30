using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class RobotStateMachine : GenericStateMachine<RobotController>
    {
        public RobotStateMachine(RobotController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<RobotController>(this));
            States.Add(StateMachine.States.PATROLLING, new PatrollingState<RobotController>(this));
            States.Add(StateMachine.States.CHASING, new ChasingState<RobotController>(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState<RobotController>(this));
            States.Add(StateMachine.States.TELEPORTING, new TeleportingState<RobotController>(this));
            States.Add(StateMachine.States.CLONING, new CloningState<RobotController>(this));
        }
    }
}