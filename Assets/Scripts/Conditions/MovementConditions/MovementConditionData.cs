using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu (menuName = "ScriptableObjects/Conditions/MovementCondition")]
    public class MovementConditionData : UnitConditionSO
    {
        public float minMagnitude;
        public float maxMagnitude;

        public override UnitCondition GetCondition(GameObject source)
        {
            MovementLogic movementLogic = source.GetComponentInChildren<MovementLogic>();
            return new MovementCondition(this, movementLogic);
        }
    }

    public class MovementCondition : UnitCondition
    {
        private MovementConditionData data;
        private MovementLogic movementLogic;

        public MovementCondition(MovementConditionData data, MovementLogic movementLogic)
        {
            this.data = data;
            this.movementLogic = movementLogic;
        }
        public override bool Check(int stage, float duration, Vector3 passValue)
        {
            float magnitude = movementLogic.Velocity.magnitude;
            return (magnitude > data.minMagnitude && magnitude < data.maxMagnitude);
        }
    }
}