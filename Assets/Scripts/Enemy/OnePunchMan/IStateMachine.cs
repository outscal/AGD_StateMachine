namespace StatePattern.Enemy
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);
    }
}