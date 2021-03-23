using System.Collections.Generic;
using UnityEngine;
using System;
using DKH;

public class InputBehaviorSet : MonoBehaviour
{     
    public InputBehavior[] behaviors;
}

[Serializable]
public class InputBehavior
{
    public InputSource inputSource;             //Event we are looking for
    public InputParameters inputParameters;     //Type of reader to make based on its parameters
    public UnitBehavior behavior;

    public void Execute(object sender, FloatInputEventArgs e)
    {
        behavior.Run(e.currentStage, e.duration, new Vector2(e.passedValue, e.passedValue));

    }
    public void Execute(object sender, Vector2InputEventArgs e)
    {
        behavior.Run(e.currentStage, e.duration, e.passedValue);
    }
}

public enum InputSource
{
    Movement,
    DebugLog
}

public enum InputParameters
{
    floatRangeReader,       
    floatAxisReader,       
    vector2Reader,        
}