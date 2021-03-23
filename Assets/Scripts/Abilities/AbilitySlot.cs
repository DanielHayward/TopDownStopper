using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{

    public class AbilitySlot : UnitBehavior
    {
        private AbilitySheet abilitySheet = null;
        [SerializeField] private IdSO[] gatherAbilitiesWithIDs;

        private int currentSlotIndex = 0;
        private List<int> abilityIndexes = new List<int>();
        private Ability currentAbility = null;
        private bool active;

        public override void CacheSource(GameObject source)
        {
            abilitySheet = source.GetComponentInChildren<AbilitySheet>();
        }
        void Start()
        {
            ScanAbilities();
            EquipAbility(0);
            if (abilityIndexes.Count < 1)
            {
                active = false;
            }
        }
        public void ScanAbilities()
        {
            if (abilitySheet.abilities.Count > 0)
            {
                for (int abilityIndex = 0; abilityIndex < abilitySheet.abilities.Count; abilityIndex++)
                {
                    for (int IdIndex = 0; IdIndex < gatherAbilitiesWithIDs.Length; IdIndex++)
                    {
                        if (abilitySheet.abilities[abilityIndex].HasId(gatherAbilitiesWithIDs[IdIndex]))
                        {
                            abilityIndexes.Add(abilityIndex);
                            break;
                        }
                    }
                }
            }
        }
        public void EquipAbility(int index)
        {
            if (abilityIndexes.Count > 0)
            {
                currentAbility = abilitySheet.abilities[abilityIndexes[index]];
                //for (int i = 0; i < currentAbility.skillSO.cost.Length; i++)
                //{
                //    statSheet.GetStat(currentAbility.skillSO.cost[i].stat).AddListener(AmmoQuantityChanged);
                //}

                //switch (currentAbility.targetingType)
                //{
                //    case AbilityTargetingTypes.TargetSelf:
                //        //targets.Clear();
                //        //targets.Add(source);
                //        break;
                //        //case AbilityTargetingTypes.TargetStoredSet:
                //        //    //skillSheet.UseSkill(currentAbility, currentAbility.targetSet);
                //        //    break;
                //        //case AbilityTargetingTypes.UseTargeter:
                //        //    //Collider2D[] collider2ds = currentAbility.targeter.GetTargets(currentAbility.targetMask, currentAbility.checkDistance);
                //        //    //GameObject[] targeterGOS = new GameObject[collider2ds.Length];
                //        //    //for (int i = 0; i < collider2ds.Length; i++)
                //        //    //{
                //        //    //    targeterGOS[i] = collider2ds[i].gameObject;
                //        //    //}
                //        //    //skillSheet.UseSkill(currentAbility, targeterGOS);
                //        //    break;
                //        //case AbilityTargetingTypes.UseMousePos:
                //        //    //Vector3[] mousePos = { DKH.Utilis.MouseUtils.GetMouseWorldPosition3D() };
                //        //    //skillSheet.UseSkill(currentAbility, mousePos);
                //        //    break;
                //        //default:
                //        //    break;
                //}

                //EventMessenger<UIEvent>.TriggerEvent("AmmoChanged" + slotID.ToString(), new UIEvent
                //{
                //    value = statSheet.GetStat(currentAbility.skillSO.cost[0].stat).GetValue(),
                //    image = currentAbility.skillSO.cost[0].stat.uiSprite
                //});
                //EventMessenger<UIEvent>.TriggerEvent("AbilityChanged" + slotID.ToString(), new UIEvent
                //{
                //    image = currentAbility.skillSO.uiSprite
                //});
            }
        }
        public void UnequipAbility(int index)
        {
            //if (abilityIndexes.Count > 0)
            //{
            //    for (int i = 0; i < currentAbility.skillSO.cost.Length; i++)
            //    {
            //        statSheet.GetStat(currentAbility.skillSO.cost[i].stat).RemoveListener(AmmoQuantityChanged);
            //    }
            currentAbility = null;
            //    EventMessenger<UIEvent>.TriggerEvent("AmmoChanged" + slotID.ToString(), new UIEvent
            //    {
            //        value = 0,
            //        image = GameAssets.Instance.blankSprite
            //    });
            //}
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }

        public void NextAbility()
        {
            if (abilityIndexes.Count > 0)
            {
                UnequipAbility(currentSlotIndex);
                currentSlotIndex++;
                if (currentSlotIndex >= abilityIndexes.Count)
                {
                    currentSlotIndex = 0;
                }
                EquipAbility(currentSlotIndex);
            }
        }

        public override void Run()
        {
            if (currentAbility != null)
            {
                abilitySheet.UseAbility(currentAbility, Vector2.zero);
            }
        }

        public override void Run(int currentStage, float duration, Vector2 inputVector)
        {
            if (currentAbility != null)
            {
                abilitySheet.UseAbility(currentAbility, inputVector, currentStage, duration);
            }
        }
    }
}