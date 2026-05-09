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

        private void Awake()
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
            foreach (var tar in target)
            {
                tar.TakeDamage(stats.Damage);
            }
        }

        public void StartAttack(HealthBase[] newTarget)
        {
            if (newTarget == null)
            {
                StopAttack();
                return;
            }

            GameManager.Instance.SetState(GameState.InCombat);

            target = newTarget;
            cooldown = 0;
            active = true;
        }

        public void StopAttack()
        {
            GameManager.Instance.SetState(GameState.Playing);
            active = false;
            target = null;
        }

        public void SetTarget(HealthBase[] healthBase) { target = healthBase; }
    }
}
