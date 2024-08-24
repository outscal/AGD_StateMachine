using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : CloneManController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateAClone();
            CreateAClone();
        }
        private void CreateAClone()
        {
            CloneManController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
            clonedRobot.SetCloneCount((Owner as CloneManController).CloneCountLeft - 1);
            clonedRobot.Teleport();
            clonedRobot.SetDefaultColor(EnemyColorType.Clone);
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }

        public void Update()
        {
            
        }

        public void OnStateExit()
        {
            
        }
    }
}

