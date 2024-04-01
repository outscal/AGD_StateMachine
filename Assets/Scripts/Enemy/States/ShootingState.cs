using UnityEngine;
namespace StatePattern.Enemy.States
{
    public class ShootingState : IState
    {
        public OnePunchManController Owner { get; set; }
        public OnePunchManStateMachine StateMachine { get; set; }
        private float timer;
        
        public ShootingState(OnePunchManStateMachine StateMachine) => this.StateMachine = StateMachine;

        public void OnStateEnter() => timer = Owner.Data.RateOfFire;

        public void Update()
        {
            Quaternion desiredRotation= CalculateRotationTowardsPlayer();
            Owner.SetRotation(RotateTowards(desiredRotation));
                
            if(IsFacingPlayer(desiredRotation))
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    timer = Owner.Data.RateOfFire;
                    Owner.Shoot();
                }
            }
        }

        public void OnStateExit() => timer = 0f;
        
        private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = Owner.Target().Position - Owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        
        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, Owner.Data.RotationSpeed / 30 * Time.deltaTime);
    }
}