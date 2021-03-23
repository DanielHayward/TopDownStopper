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
        [Header("Target Set")]
        public IdSO setID;
        [Header("Targeter")]
        public IdSO targeterID;
        public LayerMask targetMask;
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
        public IdSO[] myIDs;
        private Skill[] mySkills;
        public AbilityData data;

        private List<GameObject> targetSet = new List<GameObject>();
        private Targeter targeter;

        public void SetSource(GameObject source)
        {
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
                    //mySkills[skillIndex].SetTargets(targetSet);
                }
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

        public bool Use(Vector2 inputVector, int stage = 0, float duration = 0)
        {
            switch (data.targetingType)
            {
                case AbilityTargetingTypes.UseTargeter:
                    Collider[] colliders = targeter.GetTargets(data.targetMask, data.maxDistance);
                    List<GameObject> targeterGOS = new List<GameObject>();
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        targeterGOS.Add(colliders[i].gameObject);
                    }

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
