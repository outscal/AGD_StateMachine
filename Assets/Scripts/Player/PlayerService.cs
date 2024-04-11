using StatePattern.Main;
using UnityEngine;

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

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnPlayer);
            GameService.Instance.EventService.OnLevelEnded.AddListener(DestroyPlayer);
        }

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);

        public void SpawnPlayer(int levelId)
        {
            playerController = new PlayerController(playerScriptableObject);
        }

        public PlayerController GetPlayer() => playerController;

        private void DestroyPlayer()
        {
            playerController.DestroyPlayer();
            playerController = null;
        }
    }
}