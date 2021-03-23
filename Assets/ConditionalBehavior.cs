using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public class ConditionalBehavior : UnitBehavior
    {
        [SerializeField] private UnitConditionSO[] conditionSO;
        [SerializeField] UnitBehavior[] passBehavior;
        [SerializeField] UnitBehavior[] failBehavior;
        private UnitCondition[] conditions;

        public override void CacheSource(GameObject source)
        {
            conditions = new UnitCondition[conditionSO.Length];
            for (int dataIndex = 0; dataIndex < conditionSO.Length; dataIndex++)
            {
                conditions[dataIndex] = conditionSO[dataIndex].GetCondition(source);
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
                    for (int beahviorIndex = 0; beahviorIndex < passBehavior.Length; beahviorIndex++)
                    {
                        passBehavior[beahviorIndex].Run(stage, duration, passedValue);
                    }
                }
                else
                {
                    for (int beahviorIndex = 0; beahviorIndex < failBehavior.Length; beahviorIndex++)
                    {
                        failBehavior[beahviorIndex].Run(stage, duration, passedValue);
                    }
                }
            }
        }
    }
}

