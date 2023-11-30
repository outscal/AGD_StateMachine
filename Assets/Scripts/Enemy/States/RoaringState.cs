using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class RoaringState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public RoaringState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter(){
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_BOSS_ROAR);
            GameService.Instance.PlayerService.SlowPlayerDown(Owner.Data.SlowPlayerDownDuration);
            if(typeof(T) == typeof(BossController)){
                stateMachine.ChangeState(States.CHASING);
            }
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}