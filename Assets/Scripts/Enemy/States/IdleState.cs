using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class IdleState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private float timer;

        public IdleState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() => ResetTimer();

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (typeof(T) == typeof(OnePunchManController))
                    stateMachine.ChangeState(States.ROTATING);
                else
                    stateMachine.ChangeState(States.PATROLLING);
            }
        }

        public void OnStateExit() => timer = 0;

        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}