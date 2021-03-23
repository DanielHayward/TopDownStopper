using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DKH.Utils.Timers;

public class ResourceStatChange
{
    public ResourceStatTypeSO targetStat;
    public int rawValue;
    public int statBoostedValue;
    public int attBoostedValue;
    public int finalValue;
    public int statBoostedBonus;
    public int attBoostedBonus;
    public int amountResisted;
    public int totalDifference;
}

public class CharacterStatChange
{
    public CharacterStatTypeSO targetStat;
    public int rawValue;
    public int statBoostedValue;
    public int attBoostedValue;
    public int finalValue;
    public int statBoostedBonus;
    public int attBoostedBonus;
    public int amountResisted;
    public int totalDifference;
}

public class CharacterStatusTickEventArgs : EventArgs
{
    public string effectName;
    public StatSheet target;
    public StatSheet caster;
    public List<CharacterStatChange> targetedStatChanges;
}

public class ResourceStatusTickEventArgs : EventArgs
{
    public string effectName;
    public StatSheet target;
    public StatSheet caster;
    public List<ResourceStatChange> targetedStatChanges;
}

public class StatusEffect
{
    public event EventHandler<EventArgs> OnRemoved;


    public StatusEffectSO effectData { get; protected set; }
    protected CountingTimer timer;
    protected StatSheet target;
    protected StatSheet owner;
    
    private int currentTick = 0;

    //Add listener to allow damaging to detail its information

    public StatusEffect(StatusEffectSO effectData)
    {
        timer = new CountingTimer();
        this.effectData = effectData;
        timer.duration = effectData.tickDuration;
        timer.loop = true;
        timer.OnTimer += (object sender, EventArgs e) => Tick();
    }
    public virtual void AddListener(Action<ResourceStatusTickEventArgs> func) { }
    public virtual void AddListener(Action<CharacterStatusTickEventArgs> func) { }
    public void Apply(StatSheet target)
    {
        this.target = target;
        timer.Restart();
        //target.AddEffect(this);
        Apply();
    }

    public void Update()
    {
        timer.Update();
    }

    private void Tick()
    {
        currentTick++;
        if (currentTick == effectData.tickCount)
        {
            LastTick();
            Remove();
            //target.RemoveEffect(this);
        }
        else
        {
            Execute();
        }
    }

    public virtual void Apply()
    {
    }
    public virtual void Execute()
    {
    }
    public virtual void LastTick()
    {
    }
    //This happens when removed by any means.
    public virtual void Remove()
    {
        OnRemoved?.Invoke(this, EventArgs.Empty);
        OnRemoved = null;
    }
}
