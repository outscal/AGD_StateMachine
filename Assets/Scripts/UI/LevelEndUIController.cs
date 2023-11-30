using UnityEngine;
using UnityEngine.SceneManagement;

namespace StatePattern.UI
{
    public class LevelEndUIController : IUIController
    {
        private LevelEndUIView levelEndView;
        private const string WinResult = "Game Won";
        private const string LostResult = "Game Lost";

        public LevelEndUIController(LevelEndUIView levelEndView)
        {
            this.levelEndView = levelEndView;
            levelEndView.SetController(this);
            Hide();
        }

        public void Show() => levelEndView.EnableView();

        public void Hide() => levelEndView.DisableView();

        public void OnHomeButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        public void OnQuitButtonClicked() => Application.Quit();

        public void PlayerWon()
        {
            levelEndView.SetResultText(WinResult);
            Show();
        }

        public void PlayerLost()
        {
            levelEndView.SetResultText(LostResult);
            Show();
        }
    }
}