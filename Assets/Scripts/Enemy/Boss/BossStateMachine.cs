using StatePattern.StateMachine;

namespace StatePattern.Enemy{
    public class BossStateMachine : GenericStateMachine<BossController>{
        public BossStateMachine(BossController Owner) : base(Owner){
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<BossController>(this));
            States.Add(StateMachine.States.CHASING, new ChasingState<BossController>(this));
            States.Add(StateMachine.States.ROARING_INTIMIDATION, new RoaringState<BossController>(this));
            States.Add(StateMachine.States.QUADRUPLE_ATTACK, new QuadrupleAttackState<BossController>(this));
            States.Add(StateMachine.States.FIRE_BREATH, new FireBreathState<BossController>(this));
            States.Add(StateMachine.States.TELEPORTING, new TeleportingState<BossController>(this));
            States.Add(StateMachine.States.SUMMONING, new SummoningState<BossController>(this));
            States.Add(StateMachine.States.ULTIMATE, new UltimateState<BossController>(this));
        }
    }
}