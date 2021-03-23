using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStat : Stat
{
    public event Action<OnStatChangedEventArgs> OnChanged;

    public ResourceStatData resourceStatData;
    private int capacity;
    private int capacityBonusFromBuffs;

    public ResourceStat(ResourceStatData statData, int _level)
    {
        resourceStatData = statData;
        level = _level;
        capacity = resourceStatData.baseCapacity + (resourceStatData.capacityGrowthValue * level);
        if (resourceStatData.statType.startFull)
        {
            value = GetCapacity();
        }
        else
        {
            value = 0;
        }
        capacityBonusFromBuffs = 0;
    }
    public void SetLevel(int _level)
    {
        level = _level;
        capacity = resourceStatData.baseCapacity + (resourceStatData.capacityGrowthValue * level);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public void LevelUp(int levels = 1)
    {
        level += levels;
        capacity += (levels * resourceStatData.capacityGrowthValue);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public void Fill()
    {
        value = GetCapacity();
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public void SetValue(int amount)
    {
        value = Mathf.Clamp(amount, resourceStatData.statType.min, GetCapacity());
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public void ChangeValue(int amount)
    {
        value = Mathf.Clamp(value + amount, resourceStatData.statType.min, GetCapacity());
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public override int GetValue()
    {
        return value;
    }
    public void SetBonusCapacityFromBuffs(int amount)
    {
        capacityBonusFromBuffs = amount;
        value = Mathf.Clamp(value, resourceStatData.statType.min, GetCapacity());
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public void ChangeBonusCapacityFromBuffs(int amount)
    {
        capacityBonusFromBuffs += amount;
        value = Mathf.Clamp(value, resourceStatData.statType.min, GetCapacity());
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = resourceStatData.statType, value = value, capacity = GetCapacity() });
    }
    public override int GetCapacity()
    {
        return Mathf.Clamp(capacity + capacityBonusFromBuffs, resourceStatData.statType.min, resourceStatData.statType.maxCapacity);
    }
    public override string ToString()
    {
        return resourceStatData.statType.name + ", Level: " + level + ", Value: " + value + ", Capacity: " + capacity + ", CapacityBuffBonus: " + capacityBonusFromBuffs;
    }

    public override void AddListener(Action<OnStatChangedEventArgs> func)
    {
        OnChanged += func;
    }
    public override void RemoveListener(Action<OnStatChangedEventArgs> func)
    {
        OnChanged -= func;
    }
}
