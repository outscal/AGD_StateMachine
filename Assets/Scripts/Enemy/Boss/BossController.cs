using StatePattern.Main;
using StatePattern.Player;
using StatePattern.Sound;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy{
    public class BossController : EnemyController{
        private BossStateMachine stateMachine;

        private bool summonedAt25 = false;
        private bool summonedAt50 = false;
        private bool summonedAt75 = false;

        public BossController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new BossStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            if(!enemyAlerted){
                base.PlayerEnteredRange(targetToSet);
                stateMachine.ChangeState(States.ROARING_INTIMIDATION);
            }
        }

        public override void PlayerExitedRange(){
            if(enemyAlerted){
                base.PlayerExitedRange();
                stateMachine.ChangeState(States.IDLE);
            }
        }

        public override void TakeDamage(int damageToInflict)
        {
            base.TakeDamage(damageToInflict);
            if (currentHealth <= enemyScriptableObject.MaximumHealth * 0.01)
            {
                stateMachine.ChangeState(States.ULTIMATE);
            }
            else if (currentHealth <= enemyScriptableObject.MaximumHealth * 0.75 && !summonedAt75)
            {
                stateMachine.ChangeState(States.SUMMONING);
                summonedAt75 = true;
            }
            else if (currentHealth <= enemyScriptableObject.MaximumHealth * 0.50 && !summonedAt50)
            {
                stateMachine.ChangeState(States.SUMMONING);
                summonedAt50 = true;
            }
            else if (currentHealth <= enemyScriptableObject.MaximumHealth * 0.25 && !summonedAt25)
            {
                stateMachine.ChangeState(States.SUMMONING);
                summonedAt25 = true;
            }
        }

        public override void FireBreathAttack()
        {
            base.FireBreathAttack();
            enemyView.FireBreathAttack();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_SHOOT);
            var player = GameService.Instance.PlayerService.GetPlayer();
            if(Vector3.Distance(player.Position, enemyView.transform.position) <= Data.PlayerAttackingDistance){
                player.TakeDamage(Data.FireBreathDamage);
            }
        }

        public override void QuadrupleAttack()
        {
            base.QuadrupleAttack();
            enemyView.QuadrupleAttack();
            GameService.Instance.SoundService.PlaySoundEffects(SoundType.ENEMY_SHOOT);
            var player = GameService.Instance.PlayerService.GetPlayer();
            if(Vector3.Distance(player.Position, enemyView.transform.position) <= Data.PlayerAttackingDistance){
                player.TakeDamage(Data.QuadrupleAttackDamage);
            }
        }

        protected override void Die(){
            stateMachine.ChangeState(States.IDLE);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }
}