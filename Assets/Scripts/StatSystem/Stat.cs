using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStatChangedEventArgs : EventArgs
{
    public StatTypeSO statType;
    public int value;
    public int capacity;
}

public abstract class Stat
{
    protected int level;
    protected int value;

    public abstract int GetValue();
    public abstract int GetCapacity();
    public abstract void AddListener(Action<OnStatChangedEventArgs> func);
    public abstract void RemoveListener(Action<OnStatChangedEventArgs> func);
}
