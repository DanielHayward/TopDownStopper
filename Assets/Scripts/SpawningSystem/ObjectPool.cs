using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public GameObject prefab;
    public Transform activeContainer;
    public Transform inactiveContainer;

    public List<PooledObject> pool = new List<PooledObject>();
    private Stack<PooledObject> inactivePool = new Stack<PooledObject>();

    public int InstanceCount()
    {
        return pool.Count;
    }

    public int ActiveCount()
    {
        return (pool.Count - inactivePool.Count);
    }

    public int InactiveCount()
    {
        return inactivePool.Count;
    }

    private PooledObject CreateInstance(Vector2 pos, Transform parent)
    {
        var clone = GameObject.Instantiate(prefab, parent);
        clone.transform.position = pos;

        PooledObject pooledObject = clone.AddComponent<PooledObject>();
        pooledObject.pool = this;
        pool.Add(pooledObject);

        return pooledObject;
    }

    public PooledObject NextObject(Vector2 pos, bool usePoolParent = true, int maxInstances = 999)
    {
        PooledObject poInstance = null;

        if (inactivePool.Count > 0)
        {
            poInstance = inactivePool.Pop();
            poInstance.transform.position = pos;
            poInstance.transform.parent = activeContainer;
            poInstance.Respawn();
        }
        else if (pool.Count == maxInstances && activeContainer.childCount > 0)
        {
            activeContainer.GetChild(0).GetComponent<PooledObject>().Despawn();

            poInstance = inactivePool.Pop();
            poInstance.transform.position = pos;
            poInstance.transform.parent = activeContainer;
            poInstance.Respawn();
        }

        if (!poInstance)
        {
            if (usePoolParent)
                poInstance = CreateInstance(pos, activeContainer);
            else
                poInstance = CreateInstance(pos, null);
            poInstance.InitialSpawn();
        }

        return poInstance;
    }
    public PooledObject NextObject(Vector2 pos, Transform parent)
    {
        PooledObject poInstance = null;

        if (inactivePool.Count > 0)
        {
            poInstance = inactivePool.Pop();
            poInstance.transform.position = pos;
            poInstance.Respawn();
        }

        if (!poInstance)
        {
            poInstance = CreateInstance(pos, parent);
            poInstance.InitialSpawn();
        }

        return poInstance;
    }

    public void ReturnObject(PooledObject pooledObject)
    {
        if (pooledObject != null && pooledObject.pool == this)
        {
            pooledObject.transform.SetParent(inactiveContainer);
            pooledObject.gameObject.SetActive(false);
            inactivePool.Push(pooledObject);
        }
        else
        {
            Debug.LogWarning(pooledObject.name + " was returned to a pool it wasn't spawned from! Destroying.");
            GameObject.Destroy(pooledObject);
        }
    }

}
