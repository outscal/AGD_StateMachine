using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class RobotController : EnemyController
    {
        private RobotStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }

        public RobotController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;

        private void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

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

        protected override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(States.CLONING);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }

}