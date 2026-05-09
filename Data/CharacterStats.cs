using UnityEngine;

namespace Assets.Scripts.Data
{
    [DisallowMultipleComponent]
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] private CharacterStatsData data;
        [SerializeField] private float fallbackMaxHealth = 100f;
        [SerializeField] private float fallbackDamage = 10f;
        [SerializeField] private float fallbackAttackInterval = 1f;
        [SerializeField] private float fallbackMoveSpeed = 3f;
        [SerializeField] private float fallbackAttackRange = 0.5f;

        public float MaxHealth => data != null ? data.maxHealth : fallbackMaxHealth;
        public float Damage => data != null ? data.damage : fallbackDamage;
        public float AttackInterval => data != null ? data.attackInterval : fallbackAttackInterval;
        public float MoveSpeed => data != null ? data.moveSpeed : fallbackMoveSpeed;
        public float AttackRange => data != null ? data.attackRange : fallbackAttackRange;
    }
}
