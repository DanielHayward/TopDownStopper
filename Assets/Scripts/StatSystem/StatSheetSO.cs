using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/StatsheetData")]
public class StatSheetSO : ScriptableObject
{
    public CharacterStatData[] statData;
    public ResourceStatData[] resourceStatData;

    //[Header("Used for defense.  Offensive attributes are on the attacks.")]
    //public List<AttributeTypeSO> attributes;
}
