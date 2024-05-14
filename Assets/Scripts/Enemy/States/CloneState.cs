using StatePattern.Enemy.CloneMan;
using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloneState<T> :  IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloneState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            SpawnClones();
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void OnStateExit()
        { }

        private void SpawnClones()
        {
            GameService.Instance.EnemyService.CreateClonedEnemies(Owner.Data,(CloneManController)Owner);
            GameService.Instance.EnemyService.CreateClonedEnemies(Owner.Data,(CloneManController)Owner);
        }
    }
}