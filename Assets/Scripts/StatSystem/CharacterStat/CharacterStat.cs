using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterStat : Stat
{
    public event Action<OnStatChangedEventArgs> OnChanged;

    CharacterStatData statData;
    private int bonusFromBuffs;

    public CharacterStat(CharacterStatData _statData, int _level)
    {
        statData = _statData;
        level = _level;
        value = statData.baseValue + (statData.growthValue * level);
        bonusFromBuffs = 0;
    }
    public void SetLevel(int _level)
    {
        level = _level;
        value = statData.baseValue + (statData.growthValue * level);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public void LevelUp(int levels = 1)
    {
        level += levels;
        value += (levels * statData.growthValue);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public void SetValue(int amount)
    {
        value = Mathf.Clamp(amount, statData.statType.min, statData.statType.max);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public void ChangeValue(int amount)
    {
        value = Mathf.Clamp(value + amount, statData.statType.min, statData.statType.max);
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public override int GetValue()
    {
        return value;
    }
    public int GetValueBuffed()
    {
        return Mathf.Clamp(value + GetBonusFromBuffs(), statData.statType.min, statData.statType.buffedMax);
    }
    public void SetBonusFromBuffs(int amount)
    {
        bonusFromBuffs = amount;
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public void ChangeBonusFromBuffs(int amount)
    {
        bonusFromBuffs += amount;
        OnChanged?.Invoke(new OnStatChangedEventArgs { statType = statData.statType, value = GetValueBuffed() });
    }
    public int GetBonusFromBuffs()
    {
        return Mathf.Clamp(bonusFromBuffs, statData.statType.maxNegitiveBonusFromBuffs, statData.statType.maxBonusFromBuffs);
    }
    public override string ToString()
    {
        return statData.statType.name + ", Level: " + level + ", Value: " + value + ", BuffedBy: " + bonusFromBuffs;
    }
    public override int GetCapacity()
    {
        return statData.statType.max;
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
