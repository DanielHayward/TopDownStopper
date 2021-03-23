using System;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    [Flags]
    public enum TriggerPeroids
    {
        None = 0,
        Started = 1 << 0,
        Pressed = 1 << 1,
        Released = 1 << 2,
        Threshold1 = 1 << 3,
        Threshold2 = 1 << 4,
        Threshold3 = 1 << 5,
        Threshold4 = 1 << 6,
        Threshold5 = 1 << 7,
    }

    public abstract class CommandDataSO : ScriptableObject
    {
        protected Dictionary<GameObject, Command> previousTargets = new Dictionary<GameObject, Command>();
        protected Dictionary<Vector3, Command> previousLocations = new Dictionary<Vector3, Command>();

        public TriggerPeroids firePeroids;
        public float[] thresholds;

        private void Awake()
        {
            previousTargets.Clear();
            previousLocations.Clear();
        }
        protected abstract void AddTarget(GameObject target);
        protected abstract void AddTarget(Vector3 target);

        public virtual Command GetCommand(GameObject source, GameObject target)
        {
            if (previousTargets.ContainsKey(target))
            {
                previousTargets[target].source = source;
                return previousTargets[target];
            }
            AddTarget(target);
            previousTargets[target].source = source;
            return previousTargets[target];
        }
        public virtual Command GetCommand(GameObject source, Vector3 target)
        {
            if (previousLocations.ContainsKey(target))
            {
                previousLocations[target].source = source;
                return previousLocations[target];
            }
            AddTarget(target);
            previousLocations[target].source = source;
            return previousLocations[target];
        }
    }

    public abstract class Command
    {
        public GameObject source;
        public CommandDataSO data;

        public virtual void SetValues(int stage, float duration, Vector3 passValue) { }
        public virtual void Execute() { }
    }
}