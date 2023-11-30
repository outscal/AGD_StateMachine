using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class SummoningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public SummoningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter(){ 
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_BOSS_SUMMONING);
            foreach(var enemy in Owner.Data.BossWave){
                var summonedEnemy = GameService.Instance.EnemyService.CreateEnemy(enemy);
                GameService.Instance.EnemyService.AddEnemy(summonedEnemy);
            }
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}