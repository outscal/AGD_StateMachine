using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private bool isIdle;
        private bool isRotating;

        private bool isShooting;
        private float idleTimer;
        private float shootTimer;
        private float targetRotation;
        private PlayerController target;

        private OnePunchManStateMachine stateMachine;
        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            //   InitializeVariables();
            CreateStateMachine();
            stateMachine.ChangeState(OnePunchManStates.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);
        private void InitializeVariables()
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
            idleTimer = enemyScriptableObject.IdleTime;
            shootTimer = enemyScriptableObject.RateOfFire;
        }

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
        private void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Rotation, desiredRotation) < Data.RotationThreshold;

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        
        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Rotation, desiredRotation, enemyScriptableObject.RotationSpeed / 30 * Time.deltaTime);

       
    }
}