using DKH;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpDestinationMoveLogic : MonoBehaviour, IDesinationMoveLogic 
{
    public IdSO Id;

    public Action<Vector3> OnReachDestination;
    public Vector3[] Destinations { get; set; }
    public Transform[] Targets { get; set; }
    public Vector3 Velocity { get; set; }
    public Vector2 Location => throw new NotImplementedException();

    [SerializeField] private bool loop = false;
    [SerializeField] private float duration;

    private bool paused = true;
    private int currentWaypoint = 0;
    private Vector3 targetPosition = Vector3.zero;
    //private Utilis.Timers.CountdownTimer timer = new Utilis.Timers.CountdownTimer();
    private bool ignoreHorizontalAxis;
    private bool ignoreVerticalAxis;
    private bool ignoreDepthAxis;

    public IdSO GetID()
    {
        return Id;
    }
    void IDesinationMoveLogic.AddListenerOnDestination(Action<Vector3> func)
    {
        OnReachDestination += func;
    }

    void IDesinationMoveLogic.RemoveListenerOnDestination(Action<Vector3> func)
    {
        OnReachDestination -= func;
    }

    public void Update()
    {
        if (!paused && currentWaypoint < Destinations.Length)
        {
            //timer.Update();
        }
    }

    private void Awake()
    {
        //timer.OnTimer += Move;
    }
    public void Move()
    {
        targetPosition = Destinations[currentWaypoint]; 
        if (ignoreHorizontalAxis)
        {
            targetPosition.x = transform.position.x;
        }
        if (ignoreVerticalAxis)
        {
            targetPosition.y = transform.position.y;
        }
        if (ignoreDepthAxis)
        {
            targetPosition.z = transform.position.z;
        }
        currentWaypoint++;
        if (currentWaypoint == Destinations.Length)
        {
            if (loop)
            {
                currentWaypoint = 0;
                targetPosition = Destinations[currentWaypoint];
            }
            else
            {
                paused = true;
                OnReachDestination?.Invoke(transform.localPosition);
            }
        }
        else
        {
            targetPosition = Destinations[currentWaypoint];
        }
        transform.position = targetPosition;
    }

    public void SetDestinations(Vector3[] destination, bool loop = false)
    {
        this.Destinations = destination;
        currentWaypoint = 0;
        targetPosition = Destinations[currentWaypoint];
        //timer.timeRemaining = duration;
        //timer.Start();
        paused = false;
        this.loop = loop;
    }
    public void SetDestination(Vector3 destinations)
    {
        this.Destinations = new Vector3[] { destinations };
        currentWaypoint = 0;
        targetPosition = Destinations[currentWaypoint];
        //timer.timeRemaining = duration;
        //timer.Start();
        paused = false;
    }
    public void ClearDestinations()
    {
        this.Destinations = new Vector3[] { };
    }
    public void SetDestinationWeight(float weight)
    {

    }

    public void SetTarget(Transform target)
    {
        this.Targets = new Transform[] { target };
    }
    public void SetTargets(Transform[] targets)
    {
        this.Targets = targets;
    }
    public void ClearTargets()
    {
        Targets = new Transform[] { };
    }
    public void SetTargetWeight(float weight)
    {

    }

    public void IgnoreAxis(bool ignoreHorizontalAxis, bool ignoreVerticalAxis, bool ignoreDepthAxis)
    {
        this.ignoreHorizontalAxis = ignoreHorizontalAxis;
        this.ignoreVerticalAxis = ignoreVerticalAxis;
        this.ignoreDepthAxis = ignoreDepthAxis;
    }

    public void Stop()
    {
        currentWaypoint = 0;
        //timer.StopTimer();
        ClearTargets();
        ClearDestinations();
    }

    public void PauseMovement(bool paused)
    {
        this.paused = paused;
    }

}
