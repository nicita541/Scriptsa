using Assets.Scripts.Base;
using Assets.Scripts.Core;
using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(CharacterStats))]
    public class TargetCheckPlayer : MonoBehaviour
    {

        [SerializeField] private LayerMask layerMask;

        private CharacterStats playerStats;
        private HealthBase[] health;
        private IAttack attack;

        private void Awake()
        {
            playerStats = GetComponent<CharacterStats>();
            attack = GetComponent<AttackBase>();
        }

        private void Update()
        {
            Collider2D[] enemy = Physics2D.OverlapCircleAll(
                gameObject.transform.position,
                playerStats.AttackRange,
                layerMask
                );

            if (enemy.Length > 0)
            {
                health = new HealthBase[enemy.Length];
                foreach (var en in enemy)
                {
                    health[Array.IndexOf(enemy, en)] = en.GetComponent<HealthBase>();
                }
                if (GameManager.Instance.State == GameState.Playing)
                    attack.StartAttack(health);
                else
                    attack.SetTarget(health);
            }
            else
            {
                attack.SetTarget(null);
                attack.StopAttack();
            }
        }


    }
}
