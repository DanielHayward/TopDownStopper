using System;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu (menuName ="ScriptableObjects/Items/Misc")]
    public class ItemData : ScriptableObject
    {
        public int worth;
        
        public virtual Item GetItem()
        {
            return new Item(this);
        }
    }

    [Serializable]
    public class Item
    {
        protected ItemData itemData;
        public Item(ItemData data)
        {
            itemData = data;
        }
    }

}