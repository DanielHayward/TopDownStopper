using System;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/Equippable")]
    public class EquippableData : ItemData
    {
        public override Item GetItem()
        {
            return new Equippable(this);
        }
    }

    [Serializable]
    public class Equippable : Item
    {
        EquippableData equippableData;
        private AbilitySheet source;
        public EquippableData data;

        public Equippable(EquippableData data) : base(data)
        {
            equippableData = data;
        }

        public void SetSource(AbilitySheet source)
        {
            this.source = source;
        }
        public void Equip()
        {
            //source.AddAbility(data.ability);
        }
        public void Dequip()
        {
            //source.RemoveAbility(data.ability);
        }
    }
}