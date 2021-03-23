using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/SpawnableList")]
    public class SpawnableListSO : ScriptableObject
    {
        public SpawnableDataSO[] spawnableSOs;
    }
}
