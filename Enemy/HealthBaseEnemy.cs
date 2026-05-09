using Assets.Scripts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Enemy
{
    public class HealthBaseEnemy : HealthBase
    {
        protected override void DamageYes(float damage)
        {
            base.DamageYes(damage);
            healsBar.WidthHealsBar(stats.MaxHealth, CurrentHealth);
        }
    }
}
