using System.Threading.Tasks;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using StatePattern.Sound;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyController
    {
        protected EnemyScriptableObject enemyScriptableObject;
        protected EnemyView enemyView;

        protected int currentHealth;
        protected bool enemyAlerted = false;
        protected EnemyState currentState;

        #region Getters
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => enemyScriptableObject;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;
        public int CurrentHealth => currentHealth;
        public EnemyView EnemyView => enemyView;
        #endregion


        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(enemyScriptableObject.EnemyPrefab);
            enemyView.transform.position = enemyScriptableObject.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(enemyScriptableObject.SpawnRotation);
            enemyView.SetDetectableZone(enemyScriptableObject.RangeRadius, enemyScriptableObject.RangeAngle);
        }

        private void InitializeVariables()
        {
            SetState(EnemyState.ACTIVE);
            currentHealth = enemyScriptableObject.MaximumHealth;
        }

        public void InitializeAgent()
        {
            Agent.enabled = true;
            Agent.SetDestination(enemyScriptableObject.SpawnPosition);
            Agent.speed = enemyScriptableObject.MovementSpeed;
        }

        public virtual void TakeDamage(int damageToInflict){
            currentHealth -= damageToInflict;
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_DEATH);
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        protected virtual void Die() 
        {
            GameService.Instance.EnemyService.EnemyDied(this);
            enemyView.Destroy();
        }

        public void ToggleKillOverlay(bool value) => GameService.Instance.UIService.ToggleKillOverlay(value);

        public void ShakeCamera() => GameService.Instance.UIService.ShakeCamera();

        public void SetRotation(Vector3 eulerAngles) => enemyView.transform.rotation = Quaternion.Euler(eulerAngles);

        public void SetRotation(Quaternion desiredRotation) => enemyView.transform.rotation = desiredRotation;

        public void ToggleEnemyColor(EnemyColorType colorToSet) => enemyView.ChangeColor(colorToSet);

        public virtual void Shoot()
        {
            enemyView.PlayShootingEffect();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_SHOOT);
            _ = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }

        public virtual void FireBreathAttack(){ }

        public virtual void QuadrupleAttack(){ }

        public void SetState(EnemyState stateToSet) => currentState = stateToSet;

        public virtual void PlayerEnteredRange(PlayerController targetToSet){
            enemyAlerted = true;
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_ALERT);
        }

        public virtual void PlayerExitedRange() => enemyAlerted = false;

        public virtual void UpdateEnemy() { }

        public virtual async void FreezeEnemy(int freezeTime, float freezeFactor){
            Data.MovementSpeed /= freezeFactor;
            Data.RotationSpeed /= freezeFactor;
            await Task.Delay(freezeTime * 1000);
            Data.MovementSpeed *= freezeFactor;
            Data.RotationSpeed *= freezeFactor;
        }
    }

    public enum EnemyState
    {
        ACTIVE,
        DEACTIVE
    }
}