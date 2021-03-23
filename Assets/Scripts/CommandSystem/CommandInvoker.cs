using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public static class CommandInvoker
    {
        static Queue<Command> commandBuffer = new Queue<Command>();
        static List<Command> commandHistory = new List<Command>();

        static int counter;
        private static bool paused = false;

        public static void AddCommand(Command command)
        {
            while (commandHistory.Count > counter)
            {
                commandHistory.RemoveAt(counter);
            }
            if (command != null)
            {
                commandBuffer.Enqueue(command);
            }
        }

        public static void Update()
        {
            while (commandBuffer.Count > 0 && paused == false)
            {
                Next();
            }
        }

        public static void SetPause(bool pause)
        {
            paused = pause;
        }
        //public static void Undo()
        //{
        //    if (counter > 0)
        //    {
        //        counter--;
        //        commandHistory[counter].Undo();
        //    }
        //}
        public static void Redo()
        {
            if (counter < commandHistory.Count)
            {
                commandHistory[counter].Execute();
                counter++;
            }
        }
        public static void Next()
        {
            Command com = commandBuffer.Dequeue();
            //StateMachine sourceSM = com.source.GetComponentInChildren<StateMachine>();
            //if (sourceSM != null)
            //{
            //    sourceSM.InvokeCommand(com);
            //}
            //else
            //{
                com.Execute();
            //}
            commandHistory.Add(com);
            counter++;
        }

        public static void SaveLog()
        {
            List<string> lines = new List<string>();
            foreach (Command command in commandHistory)
            {
                lines.Add(command.ToString());
            }
            System.IO.File.WriteAllLines(Application.dataPath + "/commandlog.txt", lines);
        }
    }
}