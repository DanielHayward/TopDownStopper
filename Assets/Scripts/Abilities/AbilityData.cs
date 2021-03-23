using System;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu (menuName ="ScriptableObjects/Ability")]
    public class AbilityData : ScriptableObject
    {
        public SkillSO[] mySkillData;
        public AbilityTargetingTypes targetingType;
        public float cooldownPeroid = 0;
        [Header("Target Set")]
        public IdSO setID;
        [Header("Targeter")]
        public IdSO targeterID;
        public LayerMask targetMask;
        public int maxTargets = 1;

        [Header("SourceToTarget")]
        public float maxDistance;
    }

    public enum AbilityTargetingTypes
    {
        TargetSelf,         //Will just set target to self.
        TargetStoredSet,    //Uses a set of units stored on the ability.  Can be linked in editor or added at runtime.
        UseTargeter,        //Grabs targets when run by running the targeter
        UseMousePos,        //Uses a selector on the mouse position but does not wait for confirmation click.  Think of League Of Legends' AutoCast option.
        //UseBehavior,        //Creates an interface for the player to select the targets and hooks up to its selected event.
    }

    [Serializable]
    public class Ability
    {
        private GameObject source;
        public IdSO[] myIDs;
        private Skill[] mySkills;
        public AbilityData data;

        private List<GameObject> targetSet = new List<GameObject>();
        private Targeter targeter;
        [HideInInspector] public float cooldownTimer = 0;

        public void SetSource(GameObject source)
        {
            this.source = source;
            mySkills = new Skill[data.mySkillData.Length];
            for (int skillIndex = 0; skillIndex < data.mySkillData.Length; skillIndex++)
            {
                mySkills[skillIndex] = data.mySkillData[skillIndex].GetSkill(source);
                if (data.targetingType == AbilityTargetingTypes.TargetSelf)
                {
                    mySkills[skillIndex].SetTarget(source);
                }
                else if (data.targetingType == AbilityTargetingTypes.TargetStoredSet)
                {
                    //mySkills[skillIndex].SetTargets(targetSet);
                }
                else if (data.targetingType == AbilityTargetingTypes.UseTargeter)
                {
                    targeter = IdSO.FindComponents<Targeter>(source, data.targeterID)[0];

                }
                cooldownTimer = 0;
            }
        }

        public bool SetTargets(List<GameObject> targets)
        {
            for (int skillIndex = 0; skillIndex < mySkills.Length; skillIndex++)
            {
                mySkills[skillIndex].SetTargets(targets);
            }
            return true;
        }

        public bool SetTargets(Vector3[] locations)
        {
            for (int skillIndex = 0; skillIndex < mySkills.Length; skillIndex++)
            {
                mySkills[skillIndex].SetTargets(locations);
            }
            return true;
        }

        public bool FindTargets(int maxTargets)
        {
            switch (data.targetingType)
            {
                case AbilityTargetingTypes.TargetSelf:
                    break;
                case AbilityTargetingTypes.TargetStoredSet:
                    break;
                case AbilityTargetingTypes.UseTargeter:
                    Collider[] colliders = targeter.GetTargets(data.targetMask, data.maxDistance);
                    List<GameObject> targeterGOS = new List<GameObject>();
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        targeterGOS.Add(colliders[i].gameObject);
                    }
                    targeterGOS.Sort(SortByDistance);
                    int min = Mathf.Min(maxTargets, colliders.Length);
                    targeterGOS.RemoveRange(min, targeterGOS.Count - min);

                    for (int skillIndex = 0; skillIndex < data.mySkillData.Length; skillIndex++)
                    {
                        mySkills[skillIndex].SetTargets(targeterGOS);
                    }
                    break;
                //case AbilityTargetingTypes.UseMousePos:
                //    //Vector3[] mousePos = { DKH.Utilis.MouseUtils.GetMouseWorldPosition3D() };
                //    //skill.SetTargets(mousePos);
                //    break;
                //case AbilityTargetingTypes.UseBehavior:
                //skill.SetTargets(Utilis.MouseUtils.GetMouseWorldPosition3D();
                //break;
                default:
                    break;
            }
            return true;
        }

        public int SortByDistance(GameObject obj1, GameObject obj2)
        {
            float distanceA = (obj1.transform.position - source.transform.position).magnitude;
            float distanceB = (obj2.transform.position - source.transform.position).magnitude;

            if(distanceA > distanceB)
            {
                return 1;
            }
            if(distanceA < distanceB)
            {
                return -1;
            }
            return 0;
        }

        public bool Use(Vector2 inputVector, int stage = 0, float duration = 0)
        {
            for (int skillIndex = 0; skillIndex < mySkills.Length; skillIndex++)
            {
                CalculateSkillBuffing();
                mySkills[skillIndex].UseSkill(stage, duration, inputVector);
            }
            return true;
        }
        protected virtual void CalculateSkillBuffing()
        {
            //Gets buffed by values off chached sources and updates commands values before use skill sends them out.
        }

        public bool HasId(IdSO id)
        {
            for (int i = 0; i < myIDs.Length; i++)
            {
                if (id == myIDs[i])
                {
                    return true;
                }
            }
            return false;
        }
    }

}
