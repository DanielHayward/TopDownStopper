using UnityEngine;

namespace DKH
{
    [CreateAssetMenu (menuName = "ScriptableObjects/Conditions/InputCondition")]
    public class InputConditionData : UnitConditionSO
    {
        public override UnitCondition GetCondition(GameObject source)
        {
            return new InputCondition(this);
        }
    }
    public class InputCondition : UnitCondition
    {
        private InputConditionData data;

        public InputCondition(InputConditionData data)
        {
            this.data = data;
        }
        public override bool Check(int stage, float duration, Vector3 passValue)
        {
            return (passValue.magnitude != 0);
        }
    }
}