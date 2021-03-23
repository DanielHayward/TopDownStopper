using UnityEngine.Events;
using DKH;
using UnityEngine;

public class UnityActionBehavior : UnitBehavior
{ 
    public UnityEvent events;

    public override void CacheSource(GameObject actor)
    {

    }

    public override void Run()
    {
        events.Invoke();
    }
}
