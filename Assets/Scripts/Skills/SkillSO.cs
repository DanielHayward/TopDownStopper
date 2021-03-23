using System;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Skills/Skill")]
    public class SkillSO : ScriptableObject
    {
        public Sprite uiSprite;
        public ResourceStatAmount[] cost;
        public float maxDistance = 0;
        public CommandDataSO[] commandData;
        //Buffed By Values

        public virtual Skill GetSkill(GameObject source)
        {
            return new Skill(source, this);
        }
    }

    public class Skill : ITargetUser
    {
        private GameObject source;
        private SkillSO data;
        private List<Command> commands = new List<Command>();

        public Skill(GameObject source, SkillSO commandData)
        {
            this.source = source;
            this.data = commandData;
        }
        public bool InRange(Vector3 location)
        {
            if (data.maxDistance > (location - source.transform.position).magnitude)
            {
                return true;
            }
            return false;
        }
        public void SetTarget(GameObject target)
        {
            ClearTargets();
            if(InRange(target.transform.position))
            {
                for (int commandIndex = 0; commandIndex < data.commandData.Length; commandIndex++)
                {
                    commands.Add(data.commandData[commandIndex].GetCommand(source, target));
                }
            }

        }
        public void SetTargets(List<GameObject> targets)
        {
            ClearTargets();
            for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
            {
                if (InRange(targets[targetIndex].transform.position))
                {
                    for (int commandIndex = 0; commandIndex < data.commandData.Length; commandIndex++)
                    {
                        commands.Add(data.commandData[commandIndex].GetCommand(source, targets[targetIndex]));
                    }
                }
            }
        }
        public void SetTargets(Vector3[] locations)
        {
            ClearTargets();
            for (int targetIndex = 0; targetIndex < locations.Length; targetIndex++)
            {
                if (InRange(locations[targetIndex]))
                {
                    for (int commandIndex = 0; commandIndex < data.commandData.Length; commandIndex++)
                    {
                        commands.Add(data.commandData[commandIndex].GetCommand(source, locations[targetIndex]));
                    }
                }
            }
        }
        public void ClearTargets()
        {
            commands.Clear();
        }
        public virtual bool UseSkill(int stage = 0, float duration = 0, Vector2 inputVector = default)
        {
            for (int commandIndex = 0; commandIndex < commands.Count; commandIndex++)
            {
                commands[commandIndex].SetValues(stage, duration, inputVector);
                CommandInvoker.AddCommand(commands[commandIndex]);
            }
            return true;
        }
    }
}