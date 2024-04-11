using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class ChargeState<T> :  IState where T :  EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        private Vector3 direction;
        private Ray ray;
        private RaycastHit hit;
        private PlayerController target;
        private const float Mutiplier = 4f;
        
        public ChargeState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            target = GameService.Instance.PlayerService.GetPlayer();
            ray = new Ray(Owner.Position, Owner.Transform.forward);
            direction = (target.Position - Owner.Position).normalized;
            ChargeTowardsPlayer(direction);
        }

        public void Update()
        {
            CloseToObstacles();

            if (RemainingDistance())
            {
                 Owner.TakeDamage(5);
            }
        }

        public void OnStateExit() => Owner.Agent.speed = Owner.Data.MovementSpeed;   

        private void ChargeTowardsPlayer(Vector3 direction)
        {
            Owner.SetRotation(TurnTowardsPlayer(direction));
            Owner.Agent.speed = Owner.Data.MovementSpeed * Mutiplier;
            Owner.Agent.SetDestination(target.Position);
        }

        private void CloseToObstacles()
        {
            if (Physics.Raycast(ray,out hit ,Owner.Data.ObstacleDistance,Owner.Data.ObstacleLayer))
            {
                    hit.transform.gameObject.SetActive(false);
            }
        }

        private Quaternion TurnTowardsPlayer(Vector3 direction)
        {
            direction.y = 0;
            return Quaternion.LookRotation(direction, Owner.Transform.up);
        }

        private bool RemainingDistance() => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
    }
}