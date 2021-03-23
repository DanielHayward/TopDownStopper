using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Effects/BuffingEffect")]
public class BuffingEffectSO : StatusEffectSO
{
    [Header("Stats and Tick Damage")]
    public CharacterStatAmount[] statChange;
    public AttributeTypeSO[] attributes;

    [Header("Casters's Offensive Modifications")]
    public StatModificationData[] boostingStatsAndPercents;

    [Header("Target's Defensive Modifications")]
    public StatModificationData[] defensiveStatsAndPercents;

    public override StatusEffect GetEffect(StatSheet stats, StatSheet target)
    {
        BuffingEffect statusEffect = new BuffingEffect(this);

        if (stats != null)
        {
            for (int i = 0; i < boostingStatsAndPercents.Length; i++)
            {
                StatModification rMod = new StatModification();
                if (stats.HasStat(boostingStatsAndPercents[i].stat))
                {
                    rMod.Stat = stats.GetStat(boostingStatsAndPercents[i].stat);
                    switch (boostingStatsAndPercents[i].modificationFormula)
                    {
                        case AmountModificationFormulas.Additive:
                            rMod.modFunc = (int a, int b, int c) => (int)(a + (b * c));         // Add the stat after cutting it down by the percent to use.
                            break;
                        case AmountModificationFormulas.Multiplicative:
                            rMod.modFunc = (int a, int b, int c) => (a * (b * c));              // Value =  Result of Value Multiplied by the Stat after cutting by the percent to use.
                            break;
                        case AmountModificationFormulas.MultiplicativeBonus:
                            rMod.modFunc = (int a, int b, int c) => (int)(a + (a * (b * c)));   // Value = Value + Result of Value Multiplied by the Stat after cutting by the percent to use.
                            break;
                        case AmountModificationFormulas.ValueAsPercentageOfWhole:
                            rMod.modFunc = (int a, int b, int c) => (int)(a * ((b * c) / 100.0f)); // Use the Stat itself as a percent to cut the original value by.  Can still cut stat value by percent before using.
                            break;
                        default:
                            break;
                    }
                    rMod.percentOfStatUsed = boostingStatsAndPercents[i].percentOfStatUsed;
                    statusEffect.offensiveMods.Add(rMod);
                }
            }
        }

        for (int i = 0; i < defensiveStatsAndPercents.Length; i++)
        {
            StatModification rMod = new StatModification();
            rMod.Stat = target.GetStat(defensiveStatsAndPercents[i].stat);
            switch (defensiveStatsAndPercents[i].modificationFormula)
            {
                case AmountModificationFormulas.Additive:
                    rMod.modFunc = (int a, int b, int c) => (int)(a + (b * c));         // Add the stat after cutting it down by the percent to use.
                    break;
                case AmountModificationFormulas.Multiplicative:
                    rMod.modFunc = (int a, int b, int c) => (a * (b * c));              // Value =  Result of Value Multiplied by the Stat after cutting by the percent to use.
                    break;
                case AmountModificationFormulas.MultiplicativeBonus:
                    rMod.modFunc = (int a, int b, int c) => (int)(a + (a * (b * c)));   // Value = Value + Result of Value Multiplied by the Stat after cutting by the percent to use.
                    break;
                case AmountModificationFormulas.ValueAsPercentageOfWhole:
                    rMod.modFunc = (int a, int b, int c) => (int)(a * ((b * c) / 100.0f)); // Use the Stat itself as a percent to cut the original value by.  Can still cut stat value by percent before using.
                    break;
                default:
                    break;
            }
            rMod.percentOfStatUsed = defensiveStatsAndPercents[i].percentOfStatUsed;
            statusEffect.defensiveMods.Add(rMod);
        }


        //int value = 0;
        //bool usePreviousValue = false;
        //switch (compareValueType)
        //{
        //    case CompareValueOptions.setValue:
        //        value = compareValue;
        //        break;
        //    case CompareValueOptions.maxValue:
        //        value = stats.GetStat(resourceStat).GetCapacity();
        //        break;
        //    case CompareValueOptions.previousValue:
        //        usePreviousValue = true;
        //        break;
        //    default:
        //        break;
        //}
        //switch (compareFunction)
        //{
        //    case CompareFunction.LessThan:
        //        statCondition = new StatCondition(stats.GetStat(resourceStat), value, (int a, int b) => (a < b), usePreviousValue);
        //        break;
        //    case CompareFunction.EqualTo:
        //        statCondition = new StatCondition(stats.GetStat(resourceStat), value, (int a, int b) => (a == b), usePreviousValue);
        //        break;
        //    case CompareFunction.GreaterThan:
        //        statCondition = new StatCondition(stats.GetStat(resourceStat), value, (int a, int b) => (a > b), usePreviousValue);
        //        break;
        //    default:
        //        break;
        //}
        return statusEffect;
    }
}
