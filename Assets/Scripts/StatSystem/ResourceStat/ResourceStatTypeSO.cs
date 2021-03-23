using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceStatType")]
public class ResourceStatTypeSO : StatTypeSO
{
    public bool startFull;
    public int min;
    public int maxCapacity;
}
