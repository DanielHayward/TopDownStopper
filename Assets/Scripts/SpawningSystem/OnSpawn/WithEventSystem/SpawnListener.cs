using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnListener : MonoBehaviour
{
    public UnityEvent<GameObject> parameterizedResponses;
    public UnityEvent responses;

    private void OnEnable()
    {
        EventMessenger<ObjectEventParams>.StartListening("GameObjectSpawned", Respond);
    }
    private void OnDisable()
    {
        EventMessenger<ObjectEventParams>.StopListening("GameObjectSpawned", Respond);
    }

    public void Respond(ObjectEventParams e)
    {
        parameterizedResponses?.Invoke(e.gameObject);
        responses?.Invoke();
    }
}
