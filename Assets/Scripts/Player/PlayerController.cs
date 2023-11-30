using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.Sound;
using StatePattern.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace StatePattern.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerView playerView;

        private int currentHealth;
        public int CurrentHealth {
            get => currentHealth;
            private set {
                currentHealth = Mathf.Clamp(value, 0, playerScriptableObject.MaximumHealth);
                UIService.UpdatePlayerHealth((float)currentHealth / playerScriptableObject.MaximumHealth);
            }
        }
        private int currentCoins = 0;
        public int CurrentCoins { get => currentCoins; 
            private set{
                currentCoins = value;
                UIService.UpdateCoinsCount(currentCoins);
            }
        }
        private List<EnemyController> enemiesInRange;
        public Vector3 Position => playerView.transform.position;
        public UIService UIService => GameService.Instance.UIService;
        public SoundService SoundService => GameService.Instance.SoundService;
        public EnemyService EnemyService => GameService.Instance.EnemyService;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            InitializeView();
            InitializeVariables();
        }

        private void InitializeView()
        {
            playerView = Object.Instantiate(playerScriptableObject.PlayerPrefab);
            playerView.transform.position = playerScriptableObject.SpawnPosition;
            playerView.transform.rotation = Quaternion.Euler(playerScriptableObject.SpawnRotation);
            playerView.SetController(this);
        }

        private void InitializeVariables()
        {
            CurrentCoins = 0;
            CurrentHealth = playerScriptableObject.MaximumHealth;
            enemiesInRange = new List<EnemyController>();
        }

        public void UpdatePlayer()
        {
            if(Input.GetKeyDown(KeyCode.Space) && CurrentHealth > 0)
                UpdateAttack();
        }

        public void FixedUpdatePlayer() => UpdateMovement();

        private void UpdateMovement()
        {
            if(CurrentHealth > 0){
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                float verticalInput = Input.GetAxisRaw("Vertical");

                Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

                if(movementDirection != Vector3.zero)
                {
                    RotatePlayer(movementDirection);
                    MovePlayer(movementDirection);
                }else{
                    playerView.PlayMovementAnimation(true);
                }
            }
        }

        private void RotatePlayer(Vector3 movementDirection)
        {
            float targetRotation = GetTargetRotation(movementDirection);
            playerView.transform.eulerAngles = CalculateRotationToSet(targetRotation);
        }

        private float GetTargetRotation(Vector3 movementDirection) => Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        private Vector3 CalculateRotationToSet(float targetRotation) => Vector3.up * Mathf.MoveTowardsAngle(playerView.transform.eulerAngles.y, targetRotation, playerScriptableObject.RotationSpeed * Time.deltaTime);

        private Vector3 GetMovementVector(Vector3 movementDirection) => Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * movementDirection;

        private Vector3 GetPositionToMoveAt(Vector3 moveVector) => playerView.Rigidbody.position + moveVector * playerScriptableObject.MovementSpeed * Time.deltaTime;

        private void MovePlayer(Vector3 movementDirection)
        {
            Vector3 moveVector = GetMovementVector(movementDirection);
            playerView.Move(GetPositionToMoveAt(moveVector));
        }

        private void UpdateAttack()
        {
            playerView.Attack();
            if (enemiesInRange.Count > 0)
            {
                SoundService.PlaySoundEffects(SoundType.PLAYER_ATTACK);
                for (int i = 0; i < enemiesInRange.Count; i++)
                {
                    enemiesInRange[i].TakeDamage(playerScriptableObject.MeleeDamage);
                }
            }
            else
            {
                SoundService.PlaySoundEffects(SoundType.PLAYER_SLASH);
            }  
        }

        public void TakeDamage(int damageToInflict)
        {
            CurrentHealth -= damageToInflict;
            SoundService.PlaySoundEffects(SoundType.PLAYER_HIT);
            if(CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                PlayerDied();
                EnemyService.PlayerDied();
            }
        }

        private async void PlayerDied()
        {
            playerView.PlayDeathAnimation();
            SoundService.PlaySoundEffects(SoundType.GAME_LOST);
            await Task.Delay(playerScriptableObject.DelayAfterDeath * 1000); // converting to milliseconds
            UIService.GameLost();
        }

        public void AddEnemy(EnemyController enemy) => enemiesInRange.Add(enemy);
            
        public void RemoveEnemy(EnemyController enemy) => enemiesInRange.Remove(enemy);

        #region DropCollection
        public void CollectCoin(int coinValue) => CurrentCoins += coinValue;
        public void CollectHealth(int healthValue) => CurrentHealth += healthValue;
        public void FreezeEnemies(int freezeTime, float freezeFactor) => EnemyService.FreezeEnemies(freezeTime, freezeFactor);

        #endregion

    }
}