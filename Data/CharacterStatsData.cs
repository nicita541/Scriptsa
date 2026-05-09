using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Game/Character Stats")]
    public class CharacterStatsData : ScriptableObject
    {
        public float maxHealth = 100f;
        public float damage = 10f;
        public float attackInterval = 1f;
        public float moveSpeed = 3f;
        public float attackRange = 0.5f;
    }
}
