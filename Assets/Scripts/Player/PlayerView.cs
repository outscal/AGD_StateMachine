using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.Player
{
    public class PlayerView : MonoBehaviour
    {
        public PlayerController Controller { get; private set; }
        [SerializeField] private ParticleSystem attackVFX;
        [SerializeField] private Animator animator;

        public Rigidbody Rigidbody { get; private set; }

        private void Start() => Rigidbody = GetComponent<Rigidbody>();

        public void SetController(PlayerController controllerToSet) => Controller = controllerToSet;

        private void Update() => Controller?.UpdatePlayer();

        private void FixedUpdate() => Controller?.FixedUpdatePlayer();

        public void TakeDamage(int damage) => Controller.TakeDamage(damage);

        private void PlayAttackVFX() => attackVFX.Play();

        private void PlayAttackAnimation() => animator.SetTrigger("attack");

        public void Attack(){
            PlayAttackVFX();
            PlayAttackAnimation();
        }

        public void Move(Vector3 position){
            Rigidbody.MovePosition(position);
            PlayMovementAnimation(false);
        }

        public void PlayMovementAnimation(bool isIdle) => animator.SetBool("isIdle", isIdle);

        public void PlayDeathAnimation() => animator.SetTrigger("death");

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                var enemyController = other.GetComponent<EnemyView>().Controller;
                Controller.AddEnemy(enemyController);
                enemyController.ToggleEnemyColor(EnemyColorType.Vulnerable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<EnemyView>() != null && !other.isTrigger)
            {
                var enemyController = other.GetComponent<EnemyView>().Controller;
                Controller.RemoveEnemy(enemyController);
                enemyController.ToggleEnemyColor(EnemyColorType.Default);
            }
        }

        #region Drop Collection
        public void CollectCoin(int coinValue) => Controller?.CollectCoin(coinValue);
        public void CollectHealth(int healthValue) => Controller?.CollectHealth(healthValue);
        public void CollectTeleportationPad(Vector3 teleportationPosition) => transform.position = teleportationPosition;
        public void FreezeEnemies(int freezeTime, float freezeFactor) => Controller?.FreezeEnemies(freezeTime, freezeFactor);
        #endregion
    }
}