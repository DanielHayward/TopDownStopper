using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceSpawn : MonoBehaviour, IPoolableComponent
{
    public void InitialSpawn()
    {
        EventMessenger<ObjectEventParams>.TriggerEvent("GameObjectSpawned", new ObjectEventParams { gameObject = this.gameObject });
    }

    public void Respawn()
    {
        EventMessenger<ObjectEventParams>.TriggerEvent("GameObjectSpawned", new ObjectEventParams { gameObject = this.gameObject });
    }
    public void Despawn()
    {
        EventMessenger<ObjectEventParams>.TriggerEvent("PlayableDespawned", new ObjectEventParams { gameObject = this.gameObject });
    }

}
