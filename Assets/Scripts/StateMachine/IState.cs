using StatePattern.Enemy;

namespace StatePattern.StateMachine
{
    public interface IState
    {
        public EnemyController Owner { get; set; }
        public void OnStateEnter();
        public void Update();
        public void OnStateExit();
    }
}