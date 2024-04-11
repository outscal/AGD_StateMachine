using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class DefenseState<T> :  IState where T :  EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public DefenseState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            (Owner as TitanisController).ToggleEnemyColor(EnemyColorType.Defense);
        }

        public void Update()
        { }

        public void OnStateExit()
        { }
    }
}