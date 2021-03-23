using DKH;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DKH
{
    public class UnityEventOnSpawn : MonoBehaviour, IPoolableComponent
    {
        public UnityEvent responses;
        public bool RunOnSpawn = true;
        public bool RunOnDespawn = false;

        public void InitialSpawn()
        {
            if (RunOnSpawn)
            {
                responses?.Invoke();
            }
        }

        public void Respawn()
        {
            if (RunOnSpawn)
            {
                responses?.Invoke();
            }
        }
        public void Despawn()
        {
            if (RunOnDespawn)
            {
                responses?.Invoke();
            }
        }
    }
}
