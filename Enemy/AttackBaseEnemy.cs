using Assets.Scripts.Base;

namespace Assets.Scripts.Enemy
{
    public class AttackBaseEnemy : AttackBase
    {
        private EnemyMove enemyMove;

        protected override void Awake()
        {
            base.Awake();
            enemyMove = GetComponent<EnemyMove>();
        }

        public override void StartAttack(HealthBase[] newTarget)
        {
            base.StartAttack(newTarget);

            if (newTarget != null && newTarget.Length > 0 && enemyMove != null)
            {
                enemyMove.StopMove();
            }
        }

        public override void StopAttack()
        {
            base.StopAttack();

            if (enemyMove != null)
            {
                enemyMove.ResumeMove();
            }
        }

        public override void SetTarget(HealthBase[] healthBase)
        {
            base.SetTarget(healthBase);
        }
    }
}