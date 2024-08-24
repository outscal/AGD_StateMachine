using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine cloneManStateMachine;
        public int CloneCountLeft { get; private set; }

        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.reSpawning);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            cloneManStateMachine.ChangeState(States.IDLE);
        }
        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;
        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);

        private void CreateStateMachine() => cloneManStateMachine = new CloneManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            cloneManStateMachine.Update();
        }

        // Player enters range, change to CHASING state.
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            cloneManStateMachine.ChangeState(States.CHASING);
        }

        // Player exits range, change to IDLE state.
        public override void PlayerExitedRange() => cloneManStateMachine.ChangeState(States.IDLE);

        public override void Die()
        {
            if (CloneCountLeft > 0)
                cloneManStateMachine.ChangeState(States.CLONING);
            base.Die();
        }
        public void Teleport() => cloneManStateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

    }
}

