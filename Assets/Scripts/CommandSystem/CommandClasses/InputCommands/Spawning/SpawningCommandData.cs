using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{
    public enum SpawnType
    {
        sourceHeading,
        sourceToTarget,
        targetToSource
    }

    [CreateAssetMenu(menuName = "ScriptableObjects/Commands/Spawn")]
    public class SpawningCommandData : CommandDataSO
    {
        public IdSO myHeadingLogicID;
        public SpawnableDataSO spawnable;
        public int maxInstances = 9999;
        public Vector3 positionOffset;
        public bool offsetBasedOnHeading = false;
        public SpawnType spawnType;

        protected override void AddTarget(GameObject target)
        {
            if (target == null)
            {
                return;
            }
            IHeadingLogic[] headingLogic = IdSO.FindComponents<IHeadingLogic>(target, myHeadingLogicID);
            if (headingLogic.Length == 0)
            {
                previousTargets.Add(target, new SpawningCommand(target.transform, null, this));
            }
            else
            {
                previousTargets.Add(target, new SpawningCommand(target.transform, headingLogic[0], this));
            }
        }
        protected override void AddTarget(Vector3 target)
        {
            previousLocations.Add(target, new SpawningCommand(target, null, this));
        }
    }
    public class SpawningCommand : Command
    {
        private IHeadingLogic moveLogic;
        private SpawningCommandData spawnData;
        private int stage;
        private Vector3 location = Vector3.zero;
        private Transform target = null;

        public SpawningCommand(Vector3 location, IHeadingLogic moveLogic, SpawningCommandData data)
        {
            this.location = location;
            this.moveLogic = moveLogic;
            this.data = spawnData =  data;
        }
        public SpawningCommand(Transform target, IHeadingLogic moveLogic, SpawningCommandData data)
        {
            this.target = target;
            this.moveLogic = moveLogic;
            this.data = spawnData =  data;
        }

        public override void SetValues(int stage, float duration, Vector3 passedValue)
        {
            this.stage = stage;
        }

        public override void Execute()
        {
            if(target != null)
            {
                location = target.position;
            }
            if ((data.firePeroids & (TriggerPeroids)(1 << stage)) != TriggerPeroids.None)
            {
                PooledObject po;
                switch (spawnData.spawnType)
                {
                    case SpawnType.sourceHeading:
                        po = SpawningManager.Spawn(spawnData.spawnable, location + spawnData.positionOffset, spawnData.maxInstances);
                        if (moveLogic != null)
                        {
                            po.GetComponent<IHeadingLogic>().SetHeading(moveLogic.Heading);
                        }
                        break;
                    case SpawnType.sourceToTarget:
                        po = SpawningManager.Spawn(spawnData.spawnable, source.transform.position + spawnData.positionOffset, spawnData.maxInstances);
                        po.GetComponent<IHeadingLogic>().SetHeading(target.position - source.transform.position);
                        break;
                    case SpawnType.targetToSource:
                        po = SpawningManager.Spawn(spawnData.spawnable, source.transform.position + spawnData.positionOffset, spawnData.maxInstances);
                        po.GetComponent<IHeadingLogic>().SetHeading(source.transform.position - target.position);
                        break;
                }
            }
        }
    }
}