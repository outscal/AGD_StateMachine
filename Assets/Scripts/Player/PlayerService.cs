using System.Threading.Tasks;
using StatePattern.Main;

namespace StatePattern.Player
{
    public class PlayerService
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerController playerController;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnPlayer);

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);

        public void SpawnPlayer(int levelId)
        {
            playerController = new PlayerController(playerScriptableObject);
            UnsubscribeToEvents();
        }

        public PlayerController GetPlayer() => playerController;

        public async void SlowPlayerDown(int seconds){
            playerScriptableObject.MovementSpeed /= 2;
            playerScriptableObject.RotationSpeed /= 2; 
            await Task.Delay(seconds * 1000);
            playerScriptableObject.MovementSpeed *= 2;
            playerScriptableObject.RotationSpeed *= 2;
        }
    }
}