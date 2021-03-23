using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffingEffect : StatusEffect
{
    public event EventHandler<CharacterStatusTickEventArgs> OnApply;
    //public event EventHandler<CharacterStatusTickEventArgs> OnTick;

    private BuffingEffectSO effectSO;
    public List<StatModification> defensiveMods;
    public List<StatModification> offensiveMods;
    private List<int> revertValues;

    public BuffingEffect(BuffingEffectSO effectData) : base(effectData) //, StatSheet owner <-Since effects are made when they are placed we don't need this after the SO but we'll see maybe one day we will.
    {
        effectSO = effectData;
        defensiveMods = new List<StatModification>();
        offensiveMods = new List<StatModification>();
        revertValues = new List<int>();
    }

    public override void AddListener(Action<CharacterStatusTickEventArgs> func)
    {
        OnApply += (object sender, CharacterStatusTickEventArgs e) => func(e);
    }

    public override void Apply()
    {
        base.Apply();
        List<CharacterStatChange> statChanges = new List<CharacterStatChange>();

        for (int i = 0; i < effectSO.statChange.Length; i++)
        {
            if(target.HasStat(effectSO.statChange[i].stat))
            {
                CharacterStatChange change = new CharacterStatChange();
                change.targetStat = effectSO.statChange[i].stat;
                change.rawValue = effectSO.statChange[i].amount;
                change.statBoostedValue = CalculateStatBonus(change.rawValue);
                change.statBoostedBonus = change.statBoostedValue - change.rawValue;
                change.attBoostedValue = CalculateAttBonus(change.statBoostedValue);
                change.attBoostedBonus = change.attBoostedValue - change.statBoostedValue;
                change.finalValue = CalculateStatResistance(change.attBoostedValue);
                change.amountResisted = change.finalValue - change.attBoostedValue;
                change.totalDifference = change.finalValue - change.rawValue;
                statChanges.Add(change);
                revertValues.Add(change.finalValue*-1);
                target.GetStat(effectSO.statChange[i].stat).ChangeBonusFromBuffs(change.finalValue);
            }
        }

        OnApply?.Invoke(this, new CharacterStatusTickEventArgs { effectName = this.effectSO.myName, target = this.target, caster = this.owner, targetedStatChanges = statChanges });

    }

    public override void Execute()
    {
        base.Execute();
    }

    public override void LastTick()
    {
        Execute();
        base.LastTick();
    }

    public override void Remove()
    {
        base.Remove();
        for (int i = 0; i < effectSO.statChange.Length; i++)
        {
            if(target.HasStat(effectSO.statChange[i].stat))
            {
                target.GetStat(effectSO.statChange[i].stat).ChangeBonusFromBuffs(revertValues[i]);
            }
        }
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
        for (int i = 0; i < effectSO.defensiveStatsAndPercents.Length; i++)
        {
            if (target.HasStat(effectSO.defensiveStatsAndPercents[i].stat))
            {
                value = defensiveMods[i].modFunc(value, defensiveMods[i].Stat.GetValueBuffed(), defensiveMods[i].percentOfStatUsed);
            }
        }
        return value;
    }
}
