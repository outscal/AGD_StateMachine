using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class HitmanController : EnemyController
    {
        private HitmanStateMachine stateMachine;

        public HitmanController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new HitmanStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            if(!enemyAlerted){
                base.PlayerEnteredRange(targetToSet);
                stateMachine.ChangeState(States.CHASING);
            }
        }

        public override void PlayerExitedRange(){
            if(enemyAlerted){
                base.PlayerExitedRange();
                stateMachine.ChangeState(States.IDLE);
            }
        }
    }
}