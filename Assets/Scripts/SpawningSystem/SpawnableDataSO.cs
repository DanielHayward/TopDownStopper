using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Spawnable")]
    public class SpawnableDataSO : ScriptableObject
    {
        public GameObject prefab;
        public Sprite uiSprite;
    }
}
