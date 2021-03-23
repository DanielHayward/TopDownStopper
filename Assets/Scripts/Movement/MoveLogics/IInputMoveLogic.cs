using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeadingLogic : IHaveID
{
    Vector3 Heading { get; }
    void SetHeading(Vector3 direction, bool worldSpace = true);
}

public interface IRotationSpeedLogic : IHaveID
{
    float AngularSpeed { get; }
    void SetAngularSpeed(float speed);
    void SetMaxAngularSpeed(float speed);
    void PauseAngularMovement(bool pause);
}

//Adds an amount to the current rotation of this logic unit to combine with speed or warp or whatever to get that amount rotated.
public interface IValueRotationLogic : IHaveID
{
    Vector3 Rotation { get; }
    //void RotateDegress(float degress, Axis axis, bool useWorld = false);
    void SetRotation(Vector3 eulers, bool useWorld = false);
    //void RotateRadians(float radians, Axis axis, bool useWorld = false);
    void PauseAngularMovement(bool pause);
}

//Adds rotation until destination is acquired.   If other forces fight it, it will try till successful.
public interface IDestinationRotationLogic : IHaveID
{
    Quaternion Rotation { get; }
    Quaternion EulerAngles { get; }
    void SetAngularDestination(Quaternion rotation);
    void SetAngularDestination(Vector3 rotation);
    void PauseAngularMovement(bool pause);
}

public interface IMoveSpeedLogic : IHaveID
{
    float Speed { get; }

    void SetMovementSpeed(float moveSpeed);
    void AddMovementSpeed(float moveSpeed);

    void SetVerticalSpeed(float moveSpeed);
    void AddVerticalSpeed(float moveSpeed);

    void SetHorizontalSpeed(float moveSpeed);
    void AddHorizontalSpeed(float moveSpeed);

    void SetDepthSpeed(float moveSpeed);
    void AddDepthSpeed(float moveSpeed);

    void PauseMovement(bool pause);
}

public interface IForceMoveLogic : IHaveID
{
    Vector3 Forces { get; }
    void SetVelocity(Vector3 velocity, bool worldSpace = true);
    void AddVelocity(Vector3 velocity, bool worldSpace = true);

    void Stop();
    void PauseMovement(bool pause);
}

public interface IInputMoveLogic : IHaveID
{
    Vector3 VectorInput { get; }
    Vector3 Velocity { get; }

    void SetInputDirection(Vector3 direction, bool worldSpace = true, float weight = 1);
    void Stop();
    void PauseMovement(bool pause);
}


public interface IDesinationMoveLogic : IHaveID
{
    Vector3[] Destinations { get; }
    Transform[] Targets { get; }
    Vector3 Velocity { get; }
    Vector2 Location { get; }

    void AddListenerOnDestination(Action<Vector3> func);
    void RemoveListenerOnDestination(Action<Vector3> func);

    //Uses a list of destinations as its primary targets.
    void SetDestinations(Vector3[] destination, bool loop = false);
    void SetDestination(Vector3 destination);
    void ClearDestinations();
    void SetDestinationWeight(float weight);


    //Uses a transform as its primary target.  Will prioritize over Destinations.
    void SetTarget(Transform target);
    void SetTargets(Transform[] targets);
    void ClearTargets();
    void SetTargetWeight(float weight);

    //Pause and Unpause the movement without having to reset the speed value.
    void Stop();
    void PauseMovement(bool pause);
    void IgnoreAxis(bool ignoreHorizontalAxis, bool ignoreVerticalAxis, bool ignoreDepthAxis);
}