using System;
using UnityEngine;

namespace DKH
{
    public class ConditionResultsEventArgs : EventArgs
    {
        public int conditionID;
        public bool value;
    }

    public abstract class UnitConditionSO : ScriptableObject
    {
        public abstract UnitCondition GetCondition(GameObject source);
    }

    public abstract class UnitCondition
    {
        public bool Result { get; protected set; }

        public virtual bool Check(int stage, float duration, Vector3 passValue)
        {
            return true;
        }
    }
}