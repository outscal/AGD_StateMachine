using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class UltimateState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private List<Tuple<States, float>> ultimateStatesList;
        private CancellationTokenSource cancellationTokenSource;

        public UltimateState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter(){ 
            GameService.Instance.SoundService.PlaySoundEffects(Sound.SoundType.ENEMY_BOSS_ULTIMATE);
            ultimateStatesList = new();
            AddUltimateStates();
            ExecuteUltimateStates();
        }

        private void AddUltimateStates(){
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
        }

        private async void ExecuteUltimateStates(){
            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            foreach(var state in ultimateStatesList){
                stateMachine.ChangeState(state.Item1);
                try {
                    await Task.Delay((int)(state.Item2 * 1000), token);
                } catch (TaskCanceledException) {
                    return;
                }
                if (token.IsCancellationRequested)
                    break;
            }
        }


        public void Update() { }

        public void OnStateExit() {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }
    }
}