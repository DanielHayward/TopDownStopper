using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class StatSheet : MonoBehaviour, IPoolableComponent
{
    public class StatusChangedEventArgs : EventArgs
    {
        //public StatusEffect statusEffect;
    }

    //public event EventHandler<StatusChangedEventArgs> EffectApplied;
    //public event EventHandler<StatusChangedEventArgs> EffectRemoved;

    public StatSheetSO statsheetData;
    public int level = 1;

    //make public and watch them populate in editor, then create effect class that can grab and use stats as needed.
    private Dictionary<StatTypeSO, CharacterStat> stats;
    private Dictionary<ResourceStatTypeSO, ResourceStat> resourceStats;

    //private List<StatusEffect> statusEffects;

    private void Awake()
    {
        //statusEffects = new List<StatusEffect>();
        stats = new Dictionary<StatTypeSO, CharacterStat>();
        resourceStats = new Dictionary<ResourceStatTypeSO, ResourceStat>();
        foreach (CharacterStatData stat in statsheetData.statData)
        {
            stats[stat.statType] = new CharacterStat(stat, level);
        }
        foreach (ResourceStatData stat in statsheetData.resourceStatData)
        {
            resourceStats[stat.statType] = new ResourceStat(stat, level);
        }
    }

    private void Update()
    {
        //for (int i = 0; i < statusEffects.Count; i++)
        //{
        //    statusEffects[i].Update();
        //}
    }

    //public void AddEffect(StatusEffect effect)
    //{
    //    if (!statusEffects.Contains(effect))
    //    {
    //        statusEffects.Add(effect);
    //        EffectApplied?.Invoke(this, new StatusChangedEventArgs { statusEffect = effect });
    //    }
    //}

    //public void RemoveEffect(StatusEffect effect)
    //{
    //    if (statusEffects.Contains(effect))
    //    {
    //        statusEffects.Remove(effect);
    //        EffectRemoved?.Invoke(this, new StatusChangedEventArgs { statusEffect = effect });
    //    }
    //}

    //public void ClearEffects()
    //{
    //    statusEffects.Clear();
    //}

    public void FullRestore()
    {
        ResourceStat[] resources = new ResourceStat[resourceStats.Count];
        resourceStats.Values.CopyTo(resources, 0);
        for (int i = 0; i < resources.Length; i++)
        {
            if(resources[i].resourceStatData.statType.startFull)
            {
                resources[i].Fill();
            }
        }
    }

    public void SetLevel(int _level, bool setAllStats = true)
    {
        level = _level;
        if (setAllStats)
        {
            foreach (CharacterStat stat in stats.Values)
            {
                stat.SetLevel(level);
            }
            foreach (ResourceStat stat in resourceStats.Values)
            {
                stat.SetLevel(level);
            }
        }
    }
    public int GetLevel()
    {
        return level;
    }
    public void ChangeLevel(int amount, bool setAllStats = true)
    {
        level += amount;
        if (setAllStats)
        {
            foreach (CharacterStat stat in stats.Values)
            {
                stat.SetLevel(level);
            }
            foreach (ResourceStat stat in resourceStats.Values)
            {
                stat.SetLevel(level);
            }
        }
    }
    public bool HasStat(StatTypeSO statType)
    {
        if(statType is CharacterStatTypeSO)
        {
            return HasStat(statType as CharacterStatTypeSO);
        }
        if(statType is ResourceStatTypeSO)
        {
            return HasStat(statType as ResourceStatTypeSO);
        }
        return false;
    }
    public bool HasStat(CharacterStatTypeSO statType)
    {
        if (stats.ContainsKey(statType))
        {
            return true;
        }
        return false;
    }
    public bool HasStat(ResourceStatTypeSO statType)
    {
        if (resourceStats.ContainsKey(statType))
        {
            return true;
        }
        return false;
    }
    public Stat GetStat(StatTypeSO statType)
    {
        if (statType is CharacterStatTypeSO)
        {
            return GetStat(statType as CharacterStatTypeSO);
        }
        if (statType is ResourceStatTypeSO)
        {
            return GetStat(statType as ResourceStatTypeSO);
        }
        return null;
    }
    public CharacterStat GetStat(CharacterStatTypeSO statType)
    {
        CharacterStat stat;
        if (stats.TryGetValue(statType, out stat))
        {
            return stat;
        }
        return default;
    }
    public ResourceStat GetStat(ResourceStatTypeSO statType)
    {
        ResourceStat stat;
        if (resourceStats.TryGetValue(statType, out stat))
        {
            return stat;
        }
        return default;
    }

    public bool CanAfford(ResourceStatAmount[] resourceAmounts)
    {
        foreach (ResourceStatAmount resourceAmount in resourceAmounts)
        {
            if (HasStat(resourceAmount.stat))
            {
                if (GetStat(resourceAmount.stat).GetValue() < resourceAmount.amount)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        return true;
    }

    public void SpendAmount(ResourceStatAmount[] resourceAmounts)
    {
        foreach (ResourceStatAmount resourceAmount in resourceAmounts)
        {
            GetStat(resourceAmount.stat).ChangeValue(-1 * resourceAmount.amount);
        }
    }

    public void RestoreAmount(ResourceStatAmount[] resourceAmounts)
    {
        foreach (ResourceStatAmount resourceAmount in resourceAmounts)
        {
            if (HasStat(resourceAmount.stat))
            {
                GetStat(resourceAmount.stat).ChangeValue(1 * resourceAmount.amount);
            }
        }
    }

    public void InitialSpawn()
    {

    }

    public void Respawn()
    {
        FullRestore();
    }

    public void Despawn()
    {

    }
}

[Serializable]
public class CharacterStatData
{
    public CharacterStatTypeSO statType;
    public int baseValue;
    public int growthValue;
}

[Serializable]
public class ResourceStatData
{
    public ResourceStatTypeSO statType;
    public int baseCapacity;
    public int capacityGrowthValue;
}
