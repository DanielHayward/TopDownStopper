using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DKH;

public class EventMessenger<T> where T : EventParams
{
    private static Dictionary<string, Action<T>> eventDictionary = new Dictionary<string, Action<T>>();

    public static void StartListening(string eventName, Action<T> listener)
    {
        Action<T> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))  //ContainsKey
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<T> listener)
    {
        Action<T> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, T eventParam)
    {
        Action<T> thisEvent = null;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }

    public static void Clear()
    {
        eventDictionary.Clear();
    }
}

//Abstract base class for EventMessenger.  Do not use for other purposes.  Use sealed classes for derived classes.
//Each new derived class will automatically create a static EventMessenger for that new type
//This is not optional so don't inherit from it for anything besides event parameters.
public abstract class EventParams
{

}

public sealed class FloatEventParams : EventParams
{
    public float value;
}

public sealed class ObjectEventParams : EventParams
{
    public GameObject gameObject;
}

