using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public enum MouseInputID
    {
        LeftClick = 0,
        RightClick = 1,
        MiddleClick = 2,
        MouseButton1,
        MouseButton2,
        MouseButton3,
        MouseButton4,
        MouseButton5,
        MouseButton6,
        MouseButton7,
        MouseButton8,
        MouseButton9,
        MouseButton10,
        MouseButton11,
        MouseButton12,
    }

    public struct InputDetails
    {
        public MouseInputID ID;
        public float timeStamp;
        public Vector3 value;   
    }

    public interface IMouseControls
    {
        MouseReader GetMouseReader { get; }
        bool IsExecutingLeftClick { get; }
        bool IsExecutingMiddleClick { get; }
        bool IsExecutingRightClick { get; }
        List<InputDetails> MouseHistory { get; }       //Value is the time stamp of the last click
    }
}