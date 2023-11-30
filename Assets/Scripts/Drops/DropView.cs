using StatePattern.Player;
using UnityEngine;

namespace StatePattern.Drop{
    public class DropView : MonoBehaviour
    {
        private DropController dropController;

        [SerializeField] private SpriteRenderer displayDropSprite;
        [SerializeField] private Sprite[] dropSprites;

        public void SetController(DropController dropController) => this.dropController = dropController;

        public void SetDropSprite(DropType dropType){
            displayDropSprite.sprite = dropSprites[(int)dropType];
        }

        private void OnTriggerEnter(Collider other)
        {
            if(HasHitPlayer(other))
            {
                if (other.isTrigger)
                    return;
                else 
                    dropController.PlayerHit(other.GetComponent<PlayerView>());
            }
            Destroy(gameObject);
        }

        private bool HasHitPlayer(Collider other) => other.GetComponent<PlayerView>() != null;
    }
}