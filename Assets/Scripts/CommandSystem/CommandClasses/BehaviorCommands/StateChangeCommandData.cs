using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Commands/StateChangeCommand")]
    public class StateChangeCommandData : CommandDataSO
    {
        public IdSO stateToChangeTo;

        protected override void AddTarget(GameObject target)
        {
            if (target == null)
            {
                return;
            }
            StateMachine stateMachine = target.GetComponentInChildren<StateMachine>();
            if (stateMachine == null || stateToChangeTo == null)
            {
                Debug.LogError("Cannot issue state change command without statemachine on target or state assign in CommandData.");
            }
            previousTargets.Add(target, new StateChangeCommand(stateMachine, stateToChangeTo, this));
        }
        protected override void AddTarget(Vector3 target)
        {
            Debug.LogWarning("Targetting a location with a state change.");
            return;
        }
    }

    public class StateChangeCommand : Command
    {
        private StateMachine stateMachine;
        private IdSO stateToChangeTo;
        private int stage;

        public StateChangeCommand(StateMachine stateMachine, IdSO stateToChangeTo, StateChangeCommandData data)
        {
            this.stateMachine = stateMachine;
            this.stateToChangeTo = stateToChangeTo;
            this.data = data;
        }

        public override void SetValues(int stage, float duration, Vector3 passValue)
        {
            this.stage = stage;
        }

        public override void Execute()
        {
            if ((data.firePeroids & (TriggerPeroids)(1 << stage)) != TriggerPeroids.None)
            {
                stateMachine.ChangeState(stateToChangeTo);
            }
        }
    }
}
