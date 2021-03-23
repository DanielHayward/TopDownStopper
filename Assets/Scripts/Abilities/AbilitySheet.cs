using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class AbilitySheet : MonoBehaviour, ISourceUser
    {
        private GameObject source;
        private StatSheet statSheet = null;
        public List<Ability> abilities = new List<Ability>();

        public void SetSource(GameObject source)
        {
            this.source = source;
            statSheet = source.GetComponentInChildren<StatSheet>();
            for(int i = 0; i < abilities.Count; i++)
            {
                abilities[i].SetSource(source);
            }
        }
        public void AddAbility(Ability ability)
        {
            abilities.Add(ability);
            abilities[abilities.Count-1].SetSource(source);
        }
        public void RemoveAbility(Ability ability)
        {
            abilities.Remove(ability);
        }

        public void UseAbility(int abilityIndex, Vector2 inputVector, int stage = 0, float duration = 0)
        {
            if(Time.time > abilities[abilityIndex].cooldownTimer)
            {
                abilities[abilityIndex].cooldownTimer = Time.time + abilities[abilityIndex].data.cooldownPeroid; 
                if (statSheet.CanAfford(abilities[abilityIndex].data.mySkillData[0].cost))
                {
                    if (abilities[abilityIndex].FindTargets(abilities[abilityIndex].data.maxTargets))
                    {
                        if (abilities[abilityIndex].Use(inputVector, stage, duration))
                        {
                            statSheet.SpendAmount(abilities[abilityIndex].data.mySkillData[0].cost);
                        }
                    }
                }
            }

        }
        public void UseAbility(Ability ability, Vector2 inputVector, int stage = 0, float duration = 0)
        {
            if (Time.time > ability.cooldownTimer)
            {
                ability.cooldownTimer = Time.time + ability.data.cooldownPeroid;
                if (statSheet.CanAfford(ability.data.mySkillData[0].cost))
                {
                    if (ability.FindTargets(ability.data.maxTargets))
                    {
                        if (ability.Use(inputVector, stage, duration))
                        {
                            statSheet.SpendAmount(ability.data.mySkillData[0].cost);
                        }
                    }
                }
            }
        }
        public void UseAbility(List<GameObject> targets, int abilityIndex, Vector2 inputVector, int stage = 0, float duration = 0)
        {
            if (Time.time > abilities[abilityIndex].cooldownTimer)
            {
                abilities[abilityIndex].cooldownTimer = Time.time + abilities[abilityIndex].data.cooldownPeroid;
                if (statSheet.CanAfford(abilities[abilityIndex].data.mySkillData[0].cost))
                {
                    if (abilities[abilityIndex].SetTargets(targets))
                    {
                        if (abilities[abilityIndex].Use(inputVector, stage, duration))
                        {
                            statSheet.SpendAmount(abilities[abilityIndex].data.mySkillData[0].cost);
                        }
                    }
                }
            }
        }
        public void UseAbility(Vector3[] locations, Ability ability, Vector2 inputVector, int stage = 0, float duration = 0)
        {
            if (Time.time > ability.cooldownTimer)
            {
                ability.cooldownTimer = Time.time + ability.data.cooldownPeroid;
                if (statSheet.CanAfford(ability.data.mySkillData[0].cost))
                {
                    if (ability.SetTargets(locations))
                    {
                        if (ability.Use(inputVector, stage, duration))
                        {
                            statSheet.SpendAmount(ability.data.mySkillData[0].cost);
                        }
                    }
                }
            }
        }
    }

    public class InventoryAbilityLink : MonoBehaviour
    {
        private AbilitySheet abilitySheet;
        private Inventory inventory;

        private void Awake()
        {
            foreach (ItemData itemData in inventory.items.Keys)
            {
                if (itemData is IPassive)
                {

                }
                if (itemData is IUseable)
                {

                }
            }
        }
    }
}
