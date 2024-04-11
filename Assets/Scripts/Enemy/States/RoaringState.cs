using System.Collections.Generic;
using StatePattern.Main;
using StatePattern.Sound;
using StatePattern.StateMachine;
using UnityEngine;
using DG.Tweening;

namespace StatePattern.Enemy
{
    public class RoaringState<T> :  IState where T :  EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private float maxTime;
        private const float ShakeDuration = 0.2f;
        private const float ShakeStrength = 0.2f;
        
        public RoaringState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_ROAR);
            ResetTimer();
            ShakeObstacles();
            SlowdownPlayer();
        }

        public void Update()
        {
            maxTime -= Time.deltaTime;

            if (maxTime <= 0)
                stateMachine.ChangeState(States.CHARGE);
        }

        public void OnStateExit() => GameService.Instance.PlayerService.GetPlayer().ResetPlayerSpeed();

        private void ShakeObstacles()
        {
            Collider[] obstacles;

            obstacles = Physics.OverlapSphere(Owner.Position, Owner.Data.RangeRadius,Owner.Data.ObstacleLayer);

            for (int i = 0; i < obstacles.Length; i++)
            {
                Shake(obstacles[i].gameObject);
            }
        }

        private void Shake(GameObject obstacle)
        {
            Vector3 origScale = obstacle.transform.localScale;
            Vector3 origPos = obstacle.transform.localPosition;
            
            obstacle.transform.DOShakePosition(ShakeDuration, ShakeStrength).SetEase(Ease.InCirc);
            obstacle.transform.DOShakeScale(ShakeDuration, ShakeStrength).SetEase(Ease.InCirc);
            
            obstacle.transform.localScale = origScale;
            obstacle.transform.localPosition = origPos;
        }

        private void SlowdownPlayer() => GameService.Instance.PlayerService.GetPlayer().MoveSpeed /= 4;

        private void ResetTimer() => maxTime = 1f;
    }
}