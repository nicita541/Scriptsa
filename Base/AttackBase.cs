using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Base
{
    [RequireComponent(typeof(CharacterStats))]
    public class AttackBase : MonoBehaviour, IAttack
    {
        private CharacterStats stats;
        private HealthBase[] target;
        private float cooldown = 0;
        private bool active;
        private Animator animator;

        [SerializeField] private float baseAttackSpeed = 1f; // обычная скорость атаки

        protected virtual void Awake()
        {
            stats = GetComponent<CharacterStats>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!active || target == null) return;
            if (stats.AttackInterval <= 0f) return;

            cooldown -= Time.deltaTime;

            if (cooldown <= 0f)
            {
                float animationSpeed = baseAttackSpeed / stats.AttackInterval;

                animator.SetFloat("AttackSpeed", animationSpeed);
                animator.SetTrigger("Attack");

                cooldown = stats.AttackInterval;
            }
        }

        //вызываю в анимации в момент удара
        public void AttackEvent()
        {
            if(target == null) return;

            foreach (var tar in target)
            {
                tar.TakeDamage(stats.Damage);
            }
        }

        public virtual void StartAttack(HealthBase[] newTarget)
        {
            if (newTarget == null)
            {
                StopAttack();
                return;
            }

            target = newTarget;
            cooldown = 0;
            active = true;
        }

        public virtual void StopAttack()
        {
            active = false;
            target = null;
        }

        public virtual void SetTarget(HealthBase[] healthBase) { target = healthBase; }
    }
}
