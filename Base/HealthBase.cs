using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Base
{
    [RequireComponent(typeof(CharacterStats))]
    public class HealthBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private bool destroyOnDeath = true;
        [SerializeField] private float destroyDelay;

        protected CharacterStats stats;
        protected UpdateHealsBar healsBar;

        public event Action<HealthBase> Died;
        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        private void Awake()
        {
            stats = GetComponent<CharacterStats>();
            healsBar = GetComponent<UpdateHealsBar>();
            CurrentHealth = stats.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead) return;

            DamageYes(damage);

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Died?.Invoke(this);

                if (destroyOnDeath)
                {
                    Destroy(gameObject, destroyDelay);
                }
            }
        }

        protected virtual void DamageYes(float damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        }


    }
}
