using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyController
    {
        protected EnemyScriptableObject enemyScriptableObject;
        protected EnemyView enemyView;

        protected int currentHealth;
        protected EnemyState currentState;
        public NavMeshAgent Agent => enemyView.Agent;
        public EnemyScriptableObject Data => enemyScriptableObject;
        public Transform Transform => enemyView.transform;
        public Quaternion Rotation => enemyView.transform.rotation;
        public Vector3 Position => enemyView.transform.position;
        public int NextWaypointIndex;
        public int EnemyHealth { get; private set; }


        public EnemyController(EnemyScriptableObject enemyScriptableObject)
        {
            this.enemyScriptableObject = enemyScriptableObject;
            EnemyHealth = this.enemyScriptableObject.MaximumHealth;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            enemyView = Object.Instantiate(enemyScriptableObject.EnemyPrefab);
            enemyView.transform.position = enemyScriptableObject.SpawnPosition;
            enemyView.transform.rotation = Quaternion.Euler(enemyScriptableObject.SpawnRotation);
            enemyView.SetTriggerRadius(enemyScriptableObject.RangeRadius);
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

        public virtual void Die() 
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
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_SHOOT);
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }

        public void SetState(EnemyState stateToSet) => currentState = stateToSet;

        public virtual void PlayerEnteredRange(PlayerController targetToSet) => GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_ALERT);

        public virtual void PlayerExitedRange() { }

        public virtual void UpdateEnemy() { }

        public virtual void TakeDamage(int playerDamage)
        {
            enemyView.PlayVFX();
            if (EnemyHealth < Data.DefenseHealth)
            {
                playerDamage /= 4;
            }
                EnemyHealth -= playerDamage;
        }
        
        public void ReducePlayerDamage(){}

        public void SetNextWaypoint(int index)
        {
            if (index == Data.PatrollingPoints.Count)
                index = 0;
            
            NextWaypointIndex = index;   
        }
    }

    public enum EnemyState
    {
        ACTIVE,
        DEACTIVE
    }
}