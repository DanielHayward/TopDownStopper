using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class BehaviorOnSpawn : MonoBehaviour, IPoolableComponent
    {
        [SerializeField] private UnitBehavior behavior;
        public bool RunOnSpawn = true;
        public bool RunOnDespawn = false;

        public void InitialSpawn()
        {
            if(RunOnSpawn)
            {
                behavior.Run();
            }
        }

        public void Respawn()
        {
            if (RunOnSpawn)
            {
                behavior.Run();
            }
        }
        public void Despawn()
        {
            if (RunOnDespawn)
            {
                behavior.Run();
            }
        }
    }
}