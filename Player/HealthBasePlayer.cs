using Assets.Scripts.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class HealthBasePlayer : HealthBase
    {
        [SerializeField] private Image imageHealBar;

        protected override void DamageYes(float damage)
        {
            base.DamageYes(damage);
            imageHealBar.fillAmount = CurrentHealth / stats.MaxHealth;
        }
    }
}
