using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StatePattern.UI
{
    public class GameplayUIView : MonoBehaviour, IUIView
    {
        private GameplayUIController controller;
        [SerializeField] private TextMeshProUGUI enemyCounterText;
        [SerializeField] private TextMeshProUGUI coinsCollectedText;
        [SerializeField] private Slider playerHealthSlider;
        [SerializeField] private GameObject EnemyKillOverlay;

        public void SetController(IUIController controllerToSet) => controller = controllerToSet as GameplayUIController;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void UpdateEnemyCounterText(string enemyCounter) => enemyCounterText.SetText(enemyCounter);

        public void UpdatePlayerHealthUI(float healthRatio) => playerHealthSlider.value = healthRatio;

        public void UpdateCoinsCollectedUI(string coinsCollected) => coinsCollectedText.text = coinsCollected;

        public void ToggleKillOverlay(bool value) => EnemyKillOverlay.SetActive(value);
    }
}