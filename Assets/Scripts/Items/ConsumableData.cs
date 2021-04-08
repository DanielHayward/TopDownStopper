using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Consumable")]
    public class ConsumableData : ItemData
    {
        public override Item GetItem()
        {
            return new Consumable(this);
        }
    }
    public class Consumable : Item
    {
        ConsumableData consumeableData;
        private SkillSO skills;

        public Consumable(ConsumableData data) : base(data)
        {
            consumeableData = data;
        }

        public void Use()
        {
            //skills.Use();
        }
    }

}