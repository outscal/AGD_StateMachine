using StatePattern.Player;
using UnityEngine;

namespace StatePattern.Enemy.Bullet
{
    public class BulletController 
    {
        private BulletScriptableObject bulletScriptableObject;
        private BulletView bulletView;

        public BulletController(Transform parentTransform, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletScriptableObject = bulletScriptableObject;
            InitializeView(parentTransform);
        }

        private void InitializeView(Transform parentTransform)
        {
            bulletView = Object.Instantiate(bulletScriptableObject.Prefab);
            bulletView.transform.position = parentTransform.position;
            bulletView.transform.rotation = parentTransform.rotation;
            bulletView.transform.Translate(bulletScriptableObject.SpawnPositionOffset, Space.Self);
            bulletView.SetController(this);
        }

        public void UpdateBullet() => bulletView.transform.Translate(Vector3.forward * bulletScriptableObject.Speed * Time.deltaTime, Space.Self);

        public void PlayerHit(PlayerView playerHit) => playerHit.TakeDamage(bulletScriptableObject.Damage);
    }
}