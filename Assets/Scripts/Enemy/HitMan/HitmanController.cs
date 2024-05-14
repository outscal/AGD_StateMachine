using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.HitMan
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
            if(currentState == EnemyState.DEACTIVE)
                return;
            
            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(States.TELEPORTING);
        }
    }
}