using StatePattern.Drop;
using StatePattern.Level;
using StatePattern.Main;
using StatePattern.Player;
using StatePattern.Sound;
using StatePattern.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatePattern.Enemy
{
    public class EnemyService
    {
        private SoundService SoundService => GameService.Instance.SoundService;
        private UIService UIService => GameService.Instance.UIService;
        private LevelService LevelService => GameService.Instance.LevelService;
        private PlayerService PlayerService => GameService.Instance.PlayerService;

        private List<EnemyController> activeEnemies;
        private int spawnedEnemies;

        public EnemyService()
        {
            InitializeVariables();
            SubscribeToEvents();
        }

        private void InitializeVariables() => activeEnemies = new List<EnemyController>();

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnEnemies);

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnEnemies);

        public void SpawnEnemies(int levelId)
        {
            List<EnemyScriptableObject> enemyDataForLevel = LevelService.GetEnemyDataForLevel(levelId);
            
            foreach(EnemyScriptableObject enemySO in enemyDataForLevel)
            {
                EnemyController enemy = CreateEnemy(enemySO);
                AddEnemy(enemy);
            }

            SetEnemyCount();
            UnsubscribeToEvents();
        }

        private void SetEnemyCount()
        {
            spawnedEnemies = activeEnemies.Count;
            UIService.UpdateEnemyCount(activeEnemies.Count, spawnedEnemies);
        }

        public EnemyController CreateEnemy(EnemyScriptableObject enemyScriptableObject)
        {
            EnemyController enemy = enemyScriptableObject.Type switch
            {
                EnemyType.OnePunchMan => new OnePunchManController(enemyScriptableObject),
                EnemyType.PatrolMan => new PatrolManController(enemyScriptableObject),
                EnemyType.Hitman => new HitmanController(enemyScriptableObject),
                EnemyType.Robot => new RobotController(enemyScriptableObject),
                EnemyType.Boss => new BossController(enemyScriptableObject),
                _ => new EnemyController(enemyScriptableObject),
            };
            return enemy;
        }

        public void AddEnemy(EnemyController enemy) => activeEnemies.Add(enemy);

        public async void EnemyDied(EnemyController deadEnemy)
        {
            activeEnemies.Remove(deadEnemy);
            PlayerService.GetPlayer().RemoveEnemy(deadEnemy);
            SoundService.PlaySoundEffects(SoundType.ENEMY_DEATH);
            foreach(var dropData in deadEnemy.Data.dropData){
                _ = new DropController(deadEnemy.EnemyView.transform, dropData);
            }
            UIService.UpdateEnemyCount(activeEnemies.Count, spawnedEnemies);
            if (PlayerWon()) 
            {
                SoundService.PlaySoundEffects(Sound.SoundType.GAME_WON);
                await Task.Delay(deadEnemy.Data.DelayAfterGameEnd * 1000); //converting seconds to milliseconds 
                UIService.GameWon();
            }
        }

        public void PlayerDied()
        {
            foreach(EnemyController enemy in activeEnemies)
            {
                enemy.SetState(EnemyState.DEACTIVE);
            }
        }

        private bool PlayerWon() => activeEnemies.Count == 0;

        public void FreezeEnemies(int freezeTime, float freezeFactor){
            if(activeEnemies != null && activeEnemies.Count > 0){
                foreach(var enemy in activeEnemies){
                    enemy.FreezeEnemy(freezeTime, freezeFactor);
                }
            }
        }
    }
}