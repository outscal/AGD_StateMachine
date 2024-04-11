using StatePattern.Main;
using StatePattern.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Coins;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public EnemyController Controller { get; private set; }
        [SerializeField] public NavMeshAgent Agent;
        private SphereCollider rangeTriggerCollider;
        [SerializeField] private SpriteRenderer enemyGraphic;
        [SerializeField] private SpriteRenderer detectableRange;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private List<EnemyColor> enemyColors;
        [SerializeField] private GameObject bloodStain;
        [SerializeField] private int maxSpawnCoins = 5;
        [SerializeField] private CoinView coinPrefab;
        [SerializeField] private ParticleSystem attackVFX;

        private GameObject spawnedStain;

        private void SubsribeToEvent() => GameService.Instance.EventService.OnLevelEnded.AddListener(DestroyBloodStain);
        
        private void Start()
        {
            rangeTriggerCollider = GetComponent<SphereCollider>();
            Controller?.InitializeAgent();
            SubsribeToEvent();
        }

        public void SetController(EnemyController controllerToSet) => Controller = controllerToSet;

        public void SetTriggerRadius(float radiusToSet)
        {
            SetRangeColliderRadius(radiusToSet);
            SetRangeImageRadius(radiusToSet);
        }

        private void SetRangeColliderRadius(float radiusToSet)
        {
            if (rangeTriggerCollider != null)
                rangeTriggerCollider.radius = radiusToSet;
        }

        private void SetRangeImageRadius(float radiusToSet) => detectableRange.transform.localScale = new Vector3(radiusToSet, radiusToSet, 1);

        public void PlayShootingEffect() => muzzleFlash.Play();

        private void Update() => Controller?.UpdateEnemy();

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerEnteredRange(other.GetComponent<PlayerView>().Controller);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerView>() != null && !other.isTrigger)
                Controller.PlayerExitedRange();
        }

        public void Destroy() => StartCoroutine(EnemyDeathSequence());

        private IEnumerator EnemyDeathSequence()
        {
            Controller.ToggleKillOverlay(true);
            Controller.ShakeCamera();

            yield return new WaitForSeconds(0.1f);

            spawnedStain = Instantiate(bloodStain);
            spawnedStain.transform.position = transform.position;

            for (int i = 0; i < maxSpawnCoins; i++)
            {
                CoinView coin = Instantiate(coinPrefab);
                int mult = Random.Range(-1, 1);
                coin.transform.position = transform.position +
                                          (mult* new Vector3(i*0.1f, 0, i*0.1f));
            }
            
            Controller.ToggleKillOverlay(false);

            Destroy(gameObject);
        }

        public void ChangeColor(EnemyColorType colorType) => enemyGraphic.color = enemyColors.Find(item => item.Type == colorType).Color;

        public void SetDefaultColor(EnemyColorType colorType)
        {
            EnemyColor coloToSetAsDefault = new EnemyColor();
            coloToSetAsDefault.Type = EnemyColorType.Default;
            coloToSetAsDefault.Color = enemyColors.Find(item => item.Type == colorType).Color;
            
            enemyColors.Remove(enemyColors.Find(item => item.Type == EnemyColorType.Default));
            enemyColors.Add(coloToSetAsDefault);
        }

        public void DestroyBloodStain() => Destroy(spawnedStain);

        public void PlayVFX() => attackVFX.Play();
    }

    [System.Serializable]
    public struct EnemyColor
    {
        public EnemyColorType Type;
        public Color Color;
    }

    public enum EnemyColorType
    {
        Default,
        Vulnerable,
        Clone,
        Defense
    }
}