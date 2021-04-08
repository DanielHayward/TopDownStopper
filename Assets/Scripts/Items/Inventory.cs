using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace DKH
{

    public class ItemEventArgs : EventArgs
    {
        public ItemData itemData;
        public int quantity;
    }

    public class Inventory : MonoBehaviour, ISourceUser
    {
        public UnityEvent<ItemEventArgs> OnItemAdded;
        public UnityEvent<ItemEventArgs> OnItemUsed;
        public UnityEvent<ItemEventArgs> OnItemRemoved;

        public Dictionary<ItemData, int> items { get; private set; } = new Dictionary<ItemData, int>();
        public EquipmentItems equipment = new EquipmentItems();

        private AbilitySheet abilitySheet;

        public void SetSource(GameObject source)
        {
            abilitySheet = source.GetComponentInChildren<AbilitySheet>();
            equipment.equipment[0].SetSource(abilitySheet);
            equipment.equipment[0].Equip();
        }

        public bool HasItem(ItemData item, int quantity = 1)
        {
            if (items.ContainsKey(item))
            {
                if (items[item] >= quantity)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void AddItem(ItemData item, int quantity)
        {
            if(items.ContainsKey(item))
            {
                items[item] += quantity;
            }
            else
            {
                items.Add(item, quantity);
            }
            OnItemAdded?.Invoke(new ItemEventArgs { itemData = item, quantity = quantity });
        }

        public void UseItem(ItemData item, int quantity)
        {
            if (items.ContainsKey(item))
            {
                for (int i = 0; i < quantity; i++)
                {
                    if(items[item] > 0)
                    {
                        //item.Use();
                        items[item]--;
                        OnItemUsed?.Invoke(new ItemEventArgs { itemData = item, quantity = quantity });
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (items[item] <= 0)
            {
                items.Remove(item);
            }
        }

        public void RemoveItem(ItemData item, int quantity)
        {
            if(items.ContainsKey(item))
            {
                items[item] -= quantity;
                if(items[item] <= 0)
                {
                    items.Remove(item);
                }
                OnItemRemoved?.Invoke(new ItemEventArgs { itemData = item, quantity = quantity });
            }
        }

    }

    public class InventoryGroup
    {

    }
    public class UseableItems : InventoryGroup
    {
        private Inventory inventory;
        public List<IUseable> useables = new List<IUseable>();
        

    }
    public class PassiveItems : InventoryGroup
    {

    }

    [Serializable]
    public class EquipmentItems : InventoryGroup
    {
        private Inventory inventory;
        public List<Equippable> equipment = new List<Equippable>();
        private bool equipped = false;

        public void Use(int index)
        {
            if(!equipped)
            {
                equipment[index].Equip();
            }
            else
            {
                equipment[index].Dequip();
            }
        }
    }
}