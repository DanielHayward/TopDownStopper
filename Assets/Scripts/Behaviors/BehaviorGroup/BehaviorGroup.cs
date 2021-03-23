using System;
using UnityEngine;

//A behavior that holds a group of behaviors and allows them to be run from the same run command.
namespace DKH
{
    public class BehaviorGroup : UnitBehavior
    {
        public UnitBehavior[] behaviors;

        public override void CacheSource(GameObject source)
        {
            for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
            {
                behaviors[behaviorIndex].SetSource(source);
            }
        }
        public override void Run()
        {
            for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
            {
                behaviors[behaviorIndex].Run();
            }
        }
        public override void Run(int currentStage, float duration, Vector2 vector2)
        {
            for (int behaviorIndex = 0; behaviorIndex < behaviors.Length; behaviorIndex++)
            {
                behaviors[behaviorIndex].Run(currentStage, duration, vector2);
            }
        }
    }
}
