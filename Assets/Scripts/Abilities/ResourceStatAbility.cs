using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class ResourceStatAbility// : ITargetUser, IUseable
    {
        [SerializeField] private ResourceStatAmount statChange;
        private StatSheet targetStats;

        public void AddTarget(GameObject target)
        {
            throw new System.NotImplementedException();
        }

        public void AddTargets(List<GameObject> targets)
        {
            throw new System.NotImplementedException();
        }

        public void ClearTargets()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveTarget(GameObject target)
        {
            throw new System.NotImplementedException();
        }

        public void SetTarget(GameObject gameObject)
        {
            targetStats = gameObject.GetComponent<StatSheet>();

            if (targetStats == null)
            {
                
            }
        }

        public void Use()
        {
            if(targetStats.HasStat(statChange.stat))
            {
                targetStats.GetStat(statChange.stat).ChangeValue(statChange.amount);
            }
        }
    }
}