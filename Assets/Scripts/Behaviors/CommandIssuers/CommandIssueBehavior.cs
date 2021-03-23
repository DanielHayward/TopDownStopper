using System.Collections.Generic;
using UnityEngine;
using DKH;

namespace DKH
{
    public class CommandIssueBehavior : UnitBehavior
    {
        [SerializeField] private CommandDataSO[] commandData;
        private Command[] commands;

        public override void CacheSource(GameObject source)
        {
            commands = new Command[commandData.Length];
            for (int dataIndex = 0; dataIndex < commandData.Length; dataIndex++)
            {
                commands[dataIndex] = commandData[dataIndex].GetCommand(source, source);
            }
        }

        public override void Run()
        {
            for (int commandIndex = 0; commandIndex < commands.Length; commandIndex++)
            {
                commands[commandIndex].SetValues(0, 0, Vector3.zero);
                CommandInvoker.AddCommand(commands[commandIndex]);
            }
        }

        public override void Run(int stage, float duration, Vector2 passedValue)
        {
            for (int commandIndex = 0; commandIndex < commands.Length; commandIndex++)
            {
                commands[commandIndex].SetValues(stage, duration, passedValue);
                CommandInvoker.AddCommand(commands[commandIndex]);
            }
        }
    }
}