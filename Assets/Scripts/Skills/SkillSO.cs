using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Skills/Skill")]
    public class SkillSO : ScriptableObject
    {
        public Sprite uiSprite;
        public ResourceStatAmount[] cost;
        public float cooldownPeroid = 0;
        public CommandDataSO[] commandDatas;
        //Buffed By Values

        public virtual Skill GetSkill(GameObject source)
        {
            return new Skill(source, commandDatas);
        }
    }

    public class Skill : ITargetUser
    {
        private GameObject source;
        private CommandDataSO[] commandData;
        private List<Command> commands = new List<Command>();

        public Skill(GameObject source, CommandDataSO[] commandData)
        {
            this.source = source;
            this.commandData = commandData;
        }
        public void SetTarget(GameObject target)
        {
            ClearTargets();
            for (int commandIndex = 0; commandIndex < commandData.Length; commandIndex++)
            {
                commands.Add(commandData[commandIndex].GetCommand(source, target));
            }
        }
        public void SetTargets(List<GameObject> targets)
        {
            ClearTargets();
            for (int commandIndex = 0; commandIndex < commandData.Length; commandIndex++)
            {
                for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
                {
                    commands.Add(commandData[commandIndex].GetCommand(source, targets[targetIndex]));
                }
            }
        }
        public void SetTargets(Vector3[] locations)
        {
            ClearTargets();
            for (int commandIndex = 0; commandIndex < commandData.Length; commandIndex++)
            {
                for (int targetIndex = 0; targetIndex < locations.Length; targetIndex++)
                {
                    commands.Add(commandData[commandIndex].GetCommand(source, locations[targetIndex]));
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