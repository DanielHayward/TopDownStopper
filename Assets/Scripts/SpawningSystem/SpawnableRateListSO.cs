using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpawnRateList")]
    public class SpawnableRateListSO : ScriptableObject
    {
        public SpawnableRate[] spawnableRates;
    }
}
