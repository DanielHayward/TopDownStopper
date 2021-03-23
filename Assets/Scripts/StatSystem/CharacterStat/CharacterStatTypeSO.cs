using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterStatType")]
public class CharacterStatTypeSO : StatTypeSO
{
    public int min;
    public int max;
    public int buffedMin;
    public int buffedMax;
    public int maxNegitiveBonusFromBuffs;
    public int maxBonusFromBuffs;
}
