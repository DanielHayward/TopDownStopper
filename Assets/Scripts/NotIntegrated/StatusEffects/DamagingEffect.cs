using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class DamagingEffect : StatusEffect
{
    public event EventHandler<ResourceStatusTickEventArgs> OnTick;

    //Add an event to effects to broadcast to subscribers.  Make the subscribes stop lsitening when effect is removed.

    private DamagingEffectSO effectSO;
    public List<StatModification> defensiveMods;
    public List<StatModification> offensiveMods;


    public DamagingEffect(DamagingEffectSO effectData, StatSheet caster) : base(effectData)
    {
        effectSO = effectData;
        defensiveMods = new List<StatModification>();
        offensiveMods = new List<StatModification>();
    }

    public override void AddListener(Action<ResourceStatusTickEventArgs> func) 
    {
        OnTick += (object sender, ResourceStatusTickEventArgs e) => func(e);
    }

    public override void Apply()
    {
        base.Apply();
    }

    public override void Execute()
    {
        base.Execute();

        List<ResourceStatChange> statChanges = new List<ResourceStatChange>();

        for(int i = 0; i < effectSO.statChangePerTick.Length; i++)
        {
            
            ResourceStatChange change = new ResourceStatChange();
            change.targetStat = effectSO.statChangePerTick[i].stat;
            change.rawValue = effectSO.statChangePerTick[i].amount;
            change.statBoostedValue = CalculateStatBonus(change.rawValue);
            change.statBoostedBonus = change.statBoostedValue - change.rawValue;
            change.attBoostedValue = CalculateAttBonus(change.statBoostedValue);
            change.attBoostedBonus = change.attBoostedValue - change.statBoostedValue;
            change.finalValue = CalculateStatResistance(change.attBoostedValue);
            change.amountResisted = change.finalValue - change.attBoostedValue;
            change.totalDifference = change.finalValue - change.rawValue;
            statChanges.Add(change);
            target.GetStat(effectSO.statChangePerTick[i].stat).ChangeValue(change.finalValue);
            Debug.Log(change.finalValue);
        }
        OnTick?.Invoke(this, new ResourceStatusTickEventArgs { effectName = this.effectSO.myName, target = this.target, caster = this.owner, targetedStatChanges = statChanges } );
    }
    public override void LastTick()
    {
        Execute();
        base.LastTick();
     }

    public override void Remove()
    {
        base.Remove();
    }

    private int CalculateStatBonus(int value)
    {
        for (int i = 0; i < offensiveMods.Count; i++)
        {
            value = offensiveMods[i].modFunc(value, offensiveMods[i].Stat.GetValueBuffed(), offensiveMods[i].percentOfStatUsed);
        }
        return value;
    }
    private int CalculateAttBonus(int value)
    {
        for (int i = 0; i < effectSO.attributes.Length; i++)
        {
            for (int j = 0; j < effectSO.attributes[i].defensiveAttributes.Length; j++)
            {
                //if (target.statsheetData.attributes.Contains(effectSO.attributes[i].defensiveAttributes[j].attribute))
                //{
                //    value = (int)(value * effectSO.attributes[i].defensiveAttributes[j].relationshipPercent);
                //}
            }
        }
        return value;
    }
    private int CalculateStatResistance(int value)
    {
        for(int i = 0; i < effectSO.defensiveStatsAndPercents.Length; i++)
        {
            if (target.HasStat(effectSO.defensiveStatsAndPercents[i].stat))
            {
                value = defensiveMods[i].modFunc(value, defensiveMods[i].Stat.GetValueBuffed(), defensiveMods[i].percentOfStatUsed);
            }
        }
        return value;
    }

}
