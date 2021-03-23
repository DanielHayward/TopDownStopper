using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public static class SpawningManager
    {
        private static Dictionary<SpawnableDataSO, ObjectPool> pools = new Dictionary<SpawnableDataSO, ObjectPool>();

        //First problem is active count vs instanced count
        public static Dictionary<SpawnableDataSO, int> ObjectInstances { get; private set; } = new Dictionary<SpawnableDataSO, int>();

        public static void LoadScenePools(SpawnableListSO spawnableList)
        {
            GameObject poolsRoot = new GameObject("Pools");
            foreach (SpawnableDataSO spawnable in spawnableList.spawnableSOs)
            {
                pools[spawnable] = new ObjectPool();
                pools[spawnable].prefab = spawnable.prefab;
                GameObject poolGroup = new GameObject(spawnable.name + " pool");
                poolGroup.transform.parent = poolsRoot.transform;
                pools[spawnable].activeContainer = poolGroup.transform;
                pools[spawnable].inactiveContainer = poolGroup.transform;
                ObjectInstances[spawnable] = 0; //Good place to load prespawns
            }
        }

        public static ObjectPool GetPool(SpawnableDataSO spawnable)
        {
            return pools[spawnable];
        }

        public static void UnloadScene()
        {
            pools.Clear();
            ObjectInstances.Clear();
        }

        public static void SetPoolActiveHost(SpawnableDataSO spawnable, Transform newHost)
        {
            pools[spawnable].activeContainer = newHost;
        }

        public static void ReduceActiveCount(SpawnableDataSO spawnable)
        {
            ObjectInstances[spawnable]--;
        }

        public static PooledObject Spawn(SpawnableDataSO spawnable, Vector2 pos, int maxInstances = 9999)
        {
            if (ObjectInstances[spawnable] < maxInstances)
            {
                ObjectInstances[spawnable]++;
            }
            return pools[spawnable].NextObject(pos, true, maxInstances);
            //return null;
        }
        public static void SpawnFromRates(SpawnableRate[] spawnableData, Vector2 position)
        {
            foreach (SpawnableRate spawnableRate in spawnableData)
            {
                int rand = UnityEngine.Random.Range(0, 100);
                if (rand < spawnableRate.rate)
                {
                    if (ObjectInstances[spawnableRate.spawnable] < spawnableRate.max)
                    {
                        ObjectInstances[spawnableRate.spawnable]++;
                        pools[spawnableRate.spawnable].NextObject(position);
                        return;
                    }
                }
            }
        }
    }
}
