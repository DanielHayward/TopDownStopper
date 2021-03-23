using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Equipment")]
    public class EquipmentData : ItemData
    {
        public string dispalyName;
        public Ability ability;

        public override Item GetItem()
        {
            return new Equipment();
        }
    }
}