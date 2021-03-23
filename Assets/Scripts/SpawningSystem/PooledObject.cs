using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool pool;

    protected List<IPoolableComponent> poolableComponents;

    void Awake()
    {
        var components = GetComponents<MonoBehaviour>();
        poolableComponents = new List<IPoolableComponent>();
        foreach (var component in components)
        {
            if (component is IPoolableComponent)
            {
                poolableComponents.Add(component as IPoolableComponent);
            }
        }
    }

    public void InitialSpawn()
    {
        foreach (var component in poolableComponents)
        {
            component.InitialSpawn();
        }
        gameObject.SetActive(true);
    }

    public void Respawn()
    {
        foreach (var component in poolableComponents)
        {
            component.Respawn();
        }
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        foreach (var component in poolableComponents)
        {
            component.Despawn();
        }
        gameObject.SetActive(false);
        pool.ReturnObject(this);
    }
}