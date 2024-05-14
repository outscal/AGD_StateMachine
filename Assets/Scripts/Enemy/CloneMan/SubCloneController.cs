using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.CloneMan
{
    public class SubCloneController :  EnemyController
    {
        private SubCloneStateMachine stateMachine;
        private CloneManController Owner;
        private int cloneLevel;
        
        public SubCloneController(EnemyScriptableObject enemyScriptableObject,
            CloneManController Owner) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            this.Owner = Owner;
            cloneLevel = Owner.GetCloneCount();
            CreateStateMachine();
            stateMachine.ChangeState(States.CHASING);
        }
        
        private void CreateStateMachine() => this.stateMachine = new SubCloneStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;
            
            stateMachine.Update();
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(States.PATROLLING);

        public override void Die()
        {
            Owner.UpdateCloneCount();
            base.Die();
        }
    }
}