using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class ConditionalCommandIssueBehavior : UnitBehavior
    {
        [SerializeField] private UnitConditionSO[] conditionSO;
        [SerializeField] private CommandDataSO[] passedCommandData;
        [SerializeField] private CommandDataSO[] failedCommandData;
        private UnitCondition[] conditions;
        private Command[] passedCommands;
        private Command[] failedCommands;

        public override void CacheSource(GameObject source)
        {
            conditions = new UnitCondition[conditionSO.Length];
            for (int dataIndex = 0; dataIndex < conditionSO.Length; dataIndex++)
            {
                conditions[dataIndex] = conditionSO[dataIndex].GetCondition(source);
            }

            passedCommands = new Command[passedCommandData.Length];
            for (int dataIndex = 0; dataIndex < passedCommandData.Length; dataIndex++)
            {
                passedCommands[dataIndex] = passedCommandData[dataIndex].GetCommand(source, source);
            }
            failedCommands = new Command[failedCommandData.Length];
            for (int dataIndex = 0; dataIndex < failedCommandData.Length; dataIndex++)
            {
                failedCommands[dataIndex] = failedCommandData[dataIndex].GetCommand(source, source);
            }
        }

        public override void Run()
        {
            Run(0, 0, Vector2.zero);
        }

        public override void Run(int stage, float duration, Vector2 passedValue)
        {
            if (source != null)
            {
                bool returnValue = true;
                for (int i = 0; i < conditions.Length; i++)
                {
                    if (!conditions[i].Check(stage, duration, passedValue))
                    {
                        returnValue = false;
                    }
                }
                if (returnValue)
                {
                    for (int commandIndex = 0; commandIndex < passedCommands.Length; commandIndex++)
                    {
                        passedCommands[commandIndex].SetValues(stage, duration, passedValue);
                        CommandInvoker.AddCommand(passedCommands[commandIndex]);
                    }
                }
                else
                {
                    for (int commandIndex = 0; commandIndex < failedCommands.Length; commandIndex++)
                    {
                        failedCommands[commandIndex].SetValues(stage, duration, passedValue);
                        CommandInvoker.AddCommand(failedCommands[commandIndex]);
                    }
                }
            }
        }
    }
}