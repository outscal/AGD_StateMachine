using System.Collections.Generic;
using UnityEngine;
using StatePattern.Main;
using StatePattern.Events;
using StatePattern.Enemy;

namespace StatePattern.Level
{
    public class LevelService
    {
        private List<LevelScriptableObject> levelScriptableObjects;
        private int currentLevel;
        private LevelScriptableObject levelData;
        private GameObject levelPrefab;
        
        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(LoadLevel);
            GameService.Instance.EventService.OnLevelEnded.AddListener(ClearPreviousLevel);
        }

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(LoadLevel);

        public void LoadLevel(int levelID)
        {
            currentLevel = levelID;
            levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == levelID);
            levelPrefab = Object.Instantiate(levelData.LevelPrefab);
        }

        public List<EnemyScriptableObject> GetEnemyDataForLevel(int levelId) => levelScriptableObjects.Find(level => level.ID == levelId).EnemyScriptableObjects;

        public int GetCurrentLevel() => currentLevel;

        private void ClearPreviousLevel()
        {
            if (levelPrefab != null)
            {
                Object.Destroy(levelPrefab);
            }
        }
    }
}