using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class PatrolManController : EnemyController
    {
        private PatrolManStateMachine stateMachine;

        public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new PatrolManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
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