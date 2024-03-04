using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class IdleState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private float timer;

        public IdleState(IStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() => ResetTimer();

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (Owner.GetType() == typeof(OnePunchManController))
                    stateMachine.ChangeState(States.ROTATING);
                else
                    stateMachine.ChangeState(States.PATROLLING);
            }
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}