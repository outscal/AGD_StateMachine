using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StatePattern.UI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        private GameplayUIController controller;
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private Image playerHealth;
        [SerializeField] private GameObject EnemyKillOverlay;

        public void SetController(IUIController controllerToSet) => controller = controllerToSet as GameplayUIController;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void UpdateEnemyCounterText(string enemyCounter) => enemyCounterText.SetText(enemyCounter);

        public void UpdatePlayerHealthUI(float helathRatio) => playerHealth.transform.localScale = new Vector3(helathRatio, 1, 1);
        public void UpdateCoinText(string coins) => coinText.SetText(coins);

        public void ToggleKillOverlay(bool value) => EnemyKillOverlay.SetActive(value);
    }
}