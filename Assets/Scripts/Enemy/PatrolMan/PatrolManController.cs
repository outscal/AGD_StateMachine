using StatePattern.Player;

namespace StatePattern.Enemy.PatrolMan
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

        private void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        private void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        private void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);
    }
}