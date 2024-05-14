using StatePattern.StateMachine;

namespace StatePattern.Enemy.CloneMan
{
    public class SubCloneStateMachine : GenericStateMachine<SubCloneController>
    {
        public SubCloneStateMachine(SubCloneController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }
        
        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE,new IdleState<SubCloneController>(this));
            States.Add(StateMachine.States.PATROLLING,new PatrollingState<SubCloneController>(this));
            States.Add(StateMachine.States.CHASING,new ChasingState<SubCloneController>(this));
            States.Add(StateMachine.States.SHOOTING,new ShootingState<SubCloneController>(this));
            States.Add(StateMachine.States.TELEPORTING,new TeleportingState<SubCloneController>(this));
        }
    }
}