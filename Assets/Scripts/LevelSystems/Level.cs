using System;
using System.Collections;
using UnityEngine;

namespace DKH
{
    public class Level : MonoBehaviour
    {
        public SpawnableListSO spawnables;
        //public AIGroup levelPhysicsGroup;

        public void Awake()
        {
            SpawningManager.LoadScenePools(spawnables);
        }

        //public void Update()
        //{
        //    //levelPhysicsGroup.Update();
        //}
    }
}

