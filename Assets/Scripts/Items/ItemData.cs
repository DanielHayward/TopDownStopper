using System;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu (menuName ="ScriptableObjects/Items/Misc")]
    public class ItemData : ScriptableObject
    {
        //Resource Stat Buy/Sell Values
        
        public virtual Item GetItem()
        {
            return new Item();
        }
    }

    [Serializable]
    public class Item
    {

    }

    public class Consumeable : Item
    {
        private SkillSO skills;
        public void Use()
        {
            //skills.Use();
        }
    }

    [Serializable]
    public class Equipment : Item
    {
        private AbilitySheet source;
        public EquipmentData data;

        public void SetSource(AbilitySheet source)
        {
            this.source = source;
        }
        public void Equip()
        {
            source.AddAbility(data.ability);
        }
        public void Dequip()
        {
            source.RemoveAbility(data.ability);
        }
    }
}