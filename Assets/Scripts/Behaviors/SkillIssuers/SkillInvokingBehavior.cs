using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class SkillInvokingBehavior //: GroupBehavior
    {
        //private StatSheet statSheet = null;
        //private AbilitySheet skillSheet = null;
        //[SerializeField] private SkillSO[] skillData;
        //private Dictionary<GameObject, Skill>[] skills;

        //private void OnValidate()
        //{
        //    skills = new Dictionary<GameObject, Skill>[skillData.Length];
        //    for (int dataIndex = 0; dataIndex < skillData.Length; dataIndex++)
        //    {
        //        skills[dataIndex] = new Dictionary<GameObject, Skill>();
        //    }
        //}

        //public override void CacheSource(GameObject actor)
        //{
        //    statSheet = source.GetComponentInChildren<StatSheet>();
        //    skillSheet = source.GetComponentInChildren<AbilitySheet>();
        //}

        //public override void CacheActor(GameObject actor)
        //{
        //    for (int commandIndex = 0; commandIndex < skillData.Length; commandIndex++)
        //    {
        //        skills[commandIndex].Add(actor, skillData[commandIndex].GetSkill(actor));
        //    }
        //}

        //public override void RemoveCachedActor(GameObject actor)
        //{
        //    for (int commandIndex = 0; commandIndex < skillData.Length; commandIndex++)
        //    {
        //        skills[commandIndex].Remove(actor);
        //    }
        //}

        //public override void Run()
        //{
        //    for (int dataIndex = 0; dataIndex < skillData.Length; dataIndex++)
        //    {
        //        skills[dataIndex][source].UseSkill();

        //        for (int actorIndex = 0; actorIndex < actors.Count; actorIndex++)
        //        {
        //            skills[dataIndex][actors[actorIndex]].UseSkill();
        //        }
        //    }
        //}

        //public override void Run(int currentStage, float duration, Vector2 passedValue)
        //{
        //    for (int dataIndex = 0; dataIndex < skillData.Length; dataIndex++)
        //    {
        //        skills[dataIndex][source].UseSkill();

        //        for (int actorIndex = 0; actorIndex < actors.Count; actorIndex++)
        //        {
        //            skills[dataIndex][actors[actorIndex]].UseSkill();
        //        }
        //    }
        //}
    }
}
