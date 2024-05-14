using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.CloneMan
{
    public class CloneManController :  EnemyController
    {
        private CloneManStateMachine stateMachine;
        private int cloneCount = 0;
        public const int MaxClone = 2;
        
        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);  
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);

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
        
        public override void Die()
        {
            UpdateCloneCount();
            base.Die();
        }

        public void UpdateCloneCount()
        {
            if (cloneCount != MaxClone)
            {
                cloneCount++;
                stateMachine.ChangeState(States.CLONING);
            }
        }

        public int GetCloneCount() => cloneCount;
        
        
    }
}