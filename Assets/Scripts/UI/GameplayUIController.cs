using System.Collections;
using UnityEngine;

namespace StatePattern.UI
{
    public class GameplayUIController : IUIController
    {
        private GameplayUIView gameplayView;
        private const string ENEMY_COUNTER_PREFIX = "Enemies Left:";
        private const string COINS_PREFIX = "Coins : ";

        private int coinCount;
        
        public GameplayUIController(GameplayUIView gameplayView)
        {
            this.gameplayView = gameplayView;
            gameplayView.SetController(this);
            Hide();
        }

        public void Show() => gameplayView.EnableView();

        public void Hide() => gameplayView.DisableView();

        public void SetEnemyCount(int activeEnemies, int totalEnemies) => gameplayView.UpdateEnemyCounterText($"{ENEMY_COUNTER_PREFIX} {activeEnemies} / {totalEnemies}");

        public void SetCoinCount() => gameplayView.UpdateCoinText($"{COINS_PREFIX}{++coinCount}");

        public void SetPlayerHealthUI(float healthRatio) => gameplayView.UpdatePlayerHealthUI(healthRatio);

        public void ToggleKillOverlay(bool value) => gameplayView.ToggleKillOverlay(value);
    }
}