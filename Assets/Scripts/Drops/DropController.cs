using StatePattern.Player;
using UnityEngine;

namespace StatePattern.Drop{
    public class DropController 
    {
        private DropScriptableObject dropScriptableObject;
        private DropView dropView;

        public DropController(Transform parentTransform, DropScriptableObject dropScriptableObject)
        {
            this.dropScriptableObject = dropScriptableObject;
            InitializeView(parentTransform);
        }

        private void InitializeView(Transform parentTransform)
        {
            dropView = Object.Instantiate(dropScriptableObject.dropView);
            Vector2 randomCircle = Random.insideUnitCircle * dropScriptableObject.dropRadius;
            Vector3 randomPosition = new(randomCircle.x, 0, randomCircle.y);
            var spawnPosition = parentTransform.position + randomPosition;
            dropView.transform.SetPositionAndRotation(spawnPosition, parentTransform.rotation);
            dropView.SetController(this);
            dropView.SetDropSprite(dropScriptableObject.dropType);
        }

        public void PlayerHit(PlayerView playerHit){
            switch (dropScriptableObject.dropType)
            {
                case DropType.Coin:
                    playerHit.CollectCoin(dropScriptableObject.coinValue);
                    break;
                case DropType.FreezeBomb:
                    playerHit.FreezeEnemies(dropScriptableObject.freezeTime, dropScriptableObject.freezeFactor);
                    break;
                case DropType.TeleportationPads:
                    playerHit.CollectTeleportationPad(dropScriptableObject.teleportationPosition);
                    break;
                case DropType.HealthPacks:
                    playerHit.CollectHealth(dropScriptableObject.healthValue);
                    break;
            }
        }
    }
}