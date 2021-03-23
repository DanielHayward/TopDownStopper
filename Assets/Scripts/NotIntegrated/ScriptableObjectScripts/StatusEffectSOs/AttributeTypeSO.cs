using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AttributeValue
{
    public AttributeTypeSO attribute;
    public float relationshipPercent = 1.0f;
}

[CreateAssetMenu(menuName = "ScriptableObject/Attribute")]
public class AttributeTypeSO : ScriptableObject
{
    public string myName;
    public Sprite uiSprite;

    [Header("When attacking with this attribute the list of attributes below will modify the damage by the relationship percentage.")]
    [Header("0 Percent will make this attack do no damage to the select type.  1.0 will be no change and 2.0 will double the output.")]
    [Header("These values are for the offense of this stat only and defense will be assigned from the opposing attribute.")]

    public AttributeValue[] defensiveAttributes;
}
