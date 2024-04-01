using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private float targetRotation;
        private PlayerController target;
        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(OnePunchManStates.IDLE);
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
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(OnePunchManStates.SHOOTING);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(OnePunchManStates.IDLE);

        public PlayerController Target() => target;
    }
}