using StatePattern.CameraEffects;
using StatePattern.Main;
using UnityEngine;

namespace StatePattern.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Level Selection UI")]
        private LevelSelectionUIController levelSelectionController;
        [SerializeField] private LevelSelectionUIView levelSelectionView;
        [SerializeField] private LevelButtonView levelButtonPrefab;

        [Header("Level ENd UI")]
        private LevelEndUIController levelEndController;
        [SerializeField] private LevelEndUIView levelEndView;

        [Header("Gameplay UI")]
        private GameplayUIController gameplayController;
        [SerializeField] private GameplayUIView gameplayView;

        [Header("Camera Effects")]
        private CameraController cameraController;
        [SerializeField] private CameraScriptableObject cameraScriptableObject;

        private void Start()
        {
            cameraController = new CameraController(cameraScriptableObject);
            levelSelectionController = new LevelSelectionUIController(levelSelectionView, levelButtonPrefab);
            levelEndController = new LevelEndUIController(levelEndView);
            gameplayController = new GameplayUIController(gameplayView);
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(ShowGameplayUI);

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(ShowGameplayUI);

        public void ShowLevelSelectionUI(int levelCount) => levelSelectionController.Show(levelCount);

        private void ShowGameplayUI(int levelId) => gameplayController.Show();

        public void ToggleKillOverlay(bool value) => gameplayController.ToggleKillOverlay(value);

        public void ShakeCamera() => cameraController.ShakeCamera();

        public void GameWon()
        {
            gameplayController.Hide();
            levelEndController.PlayerWon();
        }

        public void GameLost()
        {
            gameplayController.Hide();
            levelEndController.PlayerLost();
        }

        public void UpdatePlayerHealth(float healthRatio) => gameplayController.SetPlayerHealthUI(healthRatio);

        public void UpdateEnemyCount(int activeEnemies, int totalEnemies) => gameplayController.SetEnemyCount(activeEnemies, totalEnemies);

        public void UpdateCoinsCount(int coinsCount) => gameplayController.SetCoinsCount(coinsCount);

        private void OnDestroy() => UnsubscribeToEvents();
    }
}