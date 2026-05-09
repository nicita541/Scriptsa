using Assets.Scripts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IAttack
    {
        public void StartAttack(HealthBase[] newTarget);
        public void StopAttack();

        public void SetTarget(HealthBase[] newTarget);

    }
}
