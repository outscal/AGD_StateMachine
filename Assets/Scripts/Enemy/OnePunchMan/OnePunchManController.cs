using StatePattern.StateMachine;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);

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
                stateMachine.ChangeState(States.SHOOTING);
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