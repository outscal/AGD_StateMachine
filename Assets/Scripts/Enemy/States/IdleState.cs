using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class IdleState : IState
    {
        public OnePunchManController Owner { get; set; }
        public OnePunchManStateMachine StateMachine { get; set; }
        private float timer;
        
        public IdleState(OnePunchManStateMachine StateMachine) => this.StateMachine = StateMachine;

        public void OnStateEnter() => ResetTimer();

        public void Update()
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                StateMachine.ChangeState(OnePunchManStates.ROTATION);
            }
        }

        public void OnStateExit() => timer = 0f;
        
        private void ResetTimer() => timer = Owner.Data.IdleTime;
    }
}